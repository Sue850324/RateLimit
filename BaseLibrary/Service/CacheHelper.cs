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
        public static MemoryCache _cache = MemoryCache.Default;
        const string LockKey = "Key_123";

        public static Object GetCacheObject(string key)
        {
            string _lockKey = $"{LockKey}{key}";
            lock (_cache)
            {
                if (_cache[_lockKey] == null)
                {
                    _cache.Add(_lockKey, new object(), new CacheItemPolicy() { SlidingExpiration = new TimeSpan(0, 10, 0) });
                }

                return _cache[_lockKey];
            }
        }

        public static void RefreshCache(string cacheName, object model)
        {
            Console.WriteLine($"Thread { Thread.CurrentThread.ManagedThreadId} Start Job, Now: { DateTime.Now}");
            Thread.Sleep(10000);
            _cache.Remove(cacheName);

            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1)
            };

            _cache.Add(cacheName, model, cacheItemPolicy);

            Console.WriteLine($"Thread { Thread.CurrentThread.ManagedThreadId} Stop Job, Now: { DateTime.Now}");
            Console.WriteLine("OK");
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