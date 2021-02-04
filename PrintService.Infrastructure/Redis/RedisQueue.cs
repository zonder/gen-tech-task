using System.Threading.Tasks;
using BeetleX.Redis;
using PrintService.Infrastructure.Extensions;

namespace PrintService.Infrastructure.Redis
{
    public class RedisQueue<T> : IRedisQueue<T>
    where T : class
    {
        private readonly RedisList<string> _list;

        public RedisQueue(BeetleX.Redis.RedisDB redisDb)
        {
            _list = redisDb.CreateList<string>($"L_{typeof(T).Name}");
        }

        public async Task Push(T item)
        {
            await _list.RPush(item.ToJson());
        }

        public async Task<T> Pop()
        {
            return (await _list.BLPop()).ToObject<T>();
        }
    }
}
