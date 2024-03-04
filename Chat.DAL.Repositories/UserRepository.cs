using Chat.Common.Entities;
using Chat.DAL.Repositories.Contracts;

namespace Chat.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatContext _chatContext;

        public UserRepository(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        public async Task Create(User user)
        {
            await _chatContext.Users.AddAsync(user);
        }

    }
}