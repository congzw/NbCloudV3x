namespace ZQNB.Common.Caching
{
    /// <summary>
    /// 无缓存的帮助类
    /// </summary>
    public class NullCacheManager : IPerApplicationCacheManager,IPerRequestCacheManager, ICacheManager
    {
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key)
        {
            return default(T);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        public virtual void Set(string key, object data)
        {
        }

        /// <summary>
        /// 设置缓存
        /// 已经存在则替换
        /// 默认过期时间60分钟
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheSeconds"></param>
        public void Set(string key, object data, int cacheSeconds)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public virtual bool IsSet(string key)
        {
            return false;
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
        }

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}