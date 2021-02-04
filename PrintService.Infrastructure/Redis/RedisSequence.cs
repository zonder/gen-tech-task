using System.Linq;
using System.Threading.Tasks;
using BeetleX.Redis;
using PrintService.Infrastructure.Extensions;

namespace PrintService.Infrastructure.Redis
{
    public class RedisSequence<T> : IRedisSequence<T>
    where T : class, IScoredValue
    {
        private readonly Sequence _sequence;
        public RedisSequence(BeetleX.Redis.RedisDB redisDb)
        {
            _sequence = redisDb.CreateSequence($"S_{typeof(T).Name}");
        }

        public async Task Add(T item)
        {
            await _sequence.ZAdd((item.GetScore(), item.ToJson()));
        }

        public async Task<T> GetNext()
        {
            var items = await _sequence.ZRange(0, 0, withscores: true);
            var item = items.FirstOrDefault();
            return item.Member.ToObject<T>();
        }

        public async Task Remove(T item)
        {
            await _sequence.ZRem(item.ToJson());
        }
    }
}
