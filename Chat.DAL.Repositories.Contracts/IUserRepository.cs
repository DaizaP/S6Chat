using Chat.Common.Entities;

namespace Chat.DAL.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User> GetByIdAsync(Guid id);
        Task SaveMessageToUser(Message message);
    }
}