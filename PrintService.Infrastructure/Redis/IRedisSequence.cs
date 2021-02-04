using System.Threading.Tasks;

namespace PrintService.Infrastructure.Redis
{
    public interface IRedisSequence<T>
    where T : IScoredValue
    {
        Task Add(T item);

        Task<T> GetNext();

        Task Remove(T item);
    }
}
