using System;
using System.Threading;
using System.Runtime.Caching;
using System.Web.Caching;
using CacheItemPriority = System.Web.Caching.CacheItemPriority;
using System.Web;

namespace WebRequestRateLimit.Service
{
    public class CacheHelper
    {
        public static MemoryCache CacheObject = MemoryCache.Default;
        const string LockKey = "Key_123";

        public static object GetCacheObject(string key)
        {
            string lockObject = $"{LockKey}{key}";

            lock (CacheObject)
            {
                if (CacheObject[lockObject] == null)
                {
                    CacheObject.Add(lockObject, new object(), new CacheItemPolicy() { SlidingExpiration = new TimeSpan(0, 10, 0) });
                }

                return CacheObject[lockObject];
            }
        }

        public static T Get<T>(string key, Func<T> getDataWork, TimeSpan absoluteExpireTime) where T : class
        {
            lock (GetCacheObject(key))
            {
                T result = CacheObject[key] as T;

                if (result == null)
                {
                    result = getDataWork();

                    if (result != null)
                    {
                        Set(key, result, absoluteExpireTime);
                    }
                }

                return result;
            }
        }

        public static void Set<T>(string key, T data, TimeSpan absoluteExpireTime) where T : class
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            else if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            else
            {
                DateTimeOffset expireTime = DateTimeOffset.UtcNow.Add(absoluteExpireTime);
                CacheObject.Set(key, data, expireTime);
            }
        }

        public static string GetCacheKey(string controllerName, string actionName, string identityValue)
        {

            return $"{controllerName}-{actionName}-{identityValue}";
        }

        public static void AddCache(string cacheKey, string cacheValue, CacheDependency dependency, int seconds, CacheItemRemovedCallback onRemoveCallback)
        {
            HttpRuntime.Cache.Add(
                cacheKey, 
                cacheValue, 
                dependency, 
                DateTime.Now.AddSeconds(seconds), 
                Cache.NoSlidingExpiration, 
                CacheItemPriority.Low, 
                onRemoveCallback);
        }
    }
}