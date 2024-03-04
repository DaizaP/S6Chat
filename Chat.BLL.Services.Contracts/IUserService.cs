namespace Chat.BLL.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> CheckIfExistsAsync(Guid userId);
        Task RegistrateAsync(string name, Guid userId);
    }
}