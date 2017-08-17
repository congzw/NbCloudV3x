using System;

namespace ZQNB.Common.Caching
{
    /// <summary>
    /// 缓存帮助类接口
    /// </summary>
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        T Get<T>(string key);

        /// <summary>
        /// 设置缓存
        /// 已经存在则替换
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        void Set(string key, object data);

        /// <summary>
        /// 设置缓存
        /// 已经存在则替换
        /// 默认过期时间60分钟
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheSeconds"></param>
        void Set(string key, object data, int cacheSeconds);

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        bool IsSet(string key);

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        void Remove(string key);

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// Clear all cache data
        /// </summary>
        void Clear();
    }
}
