using Chat.BLL.Services.Contracts;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.BLL.Services
{
    public class ChatHub : Hub
    {
        private readonly IUserService _userService;

        public ChatHub(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            var userIdString = Context.GetHttpContext().Request.Query["userId"].ToString();
            var userName = Context.GetHttpContext().Request.Query["name"].ToString();

            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("Please enter name");
            }

            if (string.IsNullOrEmpty(userIdString))
            {
                var newUserId = Guid.NewGuid();
                await _userService.RegistrateAsync(userName, newUserId);
                await Clients.All.SendAsync($"User {userName} was registered with id: {newUserId.ToString()}");
                return;
            }

            Guid userId = new Guid(userIdString);

            if (!await _userService.CheckIfExistsAsync(userId))
            {
                await Clients.Caller.SendAsync($"User {userName} with id: {userId} not found");
                throw new Exception("User not found");
            }

            await Clients.All.SendAsync($"User {userName} was connected");
        }

        public async Task Send(string message)
        {
            var userIdString = Context.GetHttpContext().Request.Query["userId"].ToString();
            Guid userId = new Guid(userIdString);

            await _userService.SaveMessage(message, userId);

            await Clients.All.SendAsync(message);
        }
    }
}
