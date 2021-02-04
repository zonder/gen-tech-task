using System.Threading.Tasks;

namespace PrintService.Infrastructure.Redis
{
    public interface ILockRegistry
    {
        Task<bool> AcquireLock(string key);
        Task ReleaseLock(string key);
    }
}
