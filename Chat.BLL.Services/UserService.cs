using Chat.BLL.Services.Contracts;
using Chat.Common.Entities;
using Chat.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckIfExistsAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                return false;

            return true;
        }

        public async Task RegistrateAsync(string name, Guid userId)
        {
            User user = new User
            {
                Id = userId,
                Name = name,
            };
            await _userRepository.CreateAsync(user);
        }

        public async Task SaveMessage(string content, Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var message = new Message
            {
                Id = Guid.NewGuid(),
                User = user,
                Content = content
            };

            await _userRepository.SaveMessageToUser(message);
        }
    }
}
