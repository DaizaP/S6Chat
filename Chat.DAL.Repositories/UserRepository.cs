using Chat.Common.Entities;
using Chat.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Chat.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatContext _chatContext;

        public UserRepository(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        public async Task CreateAsync(User user)
        {
            await _chatContext.Users.AddAsync(user);
            await _chatContext.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _chatContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task SaveMessageToUser(Message message)
        {
            try
            {
                await _chatContext.Messages.AddAsync(message);
                await _chatContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

    }
}