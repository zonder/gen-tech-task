using System;
using System.Threading.Tasks;
using BeetleX.Redis;

namespace PrintService.Infrastructure.Redis
{
    public class RedisLockRegistry : ILockRegistry
    {
        private readonly RedisHashTable _hashTable;

        public RedisLockRegistry(BeetleX.Redis.RedisDB redisDb)
        {
            _hashTable = redisDb.CreateHashTable("lock_registry");
        }

        public async Task<bool> AcquireLock(string key)
        {
            if (await _hashTable.Exists(key))
            {
                return false;
            }

            await _hashTable.Set(key, DateTime.UtcNow);
            return true;
        }

        public async Task ReleaseLock(string key)
        {
            await _hashTable.Del(key);
        }
    }
}
