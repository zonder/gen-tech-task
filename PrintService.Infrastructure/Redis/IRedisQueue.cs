using System.Threading.Tasks;

namespace PrintService.Infrastructure.Redis
{
    public interface IRedisQueue<T>
    {
        Task Push(T item);

        Task<T> Pop();
    }
}
