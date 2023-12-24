using Microsoft.Extensions.Caching.Memory;

namespace AD.Memory.Cache
{
    public static class CommonCache
    {
        static MemoryCache _memCache;
        static Dictionary<string, object> _keyValuePair = new Dictionary<string, object>();

        private static MemoryCache MemCache()
        {
            if (_memCache == null)
            {
                MemoryCacheOptions optn = new MemoryCacheOptions();
                _memCache = new MemoryCache(optn);
            }
            return _memCache;
        }

        public static void SetItem<T>(string key, T value, int min)
        {
            if (_memCache == null)
            {
                MemCache();
            }
            _memCache.Set(key, value, new TimeSpan(0, min, 0));
        }

        public static T GetItem<T>(string key)
        {
            if (_memCache == null)
            {
                MemCache();
            }
            return (T)_memCache.Get(key);
        }

        public static void SetItemStatic<T>(string key, T value)
        {
            if (!_keyValuePair.ContainsKey(key))
                _keyValuePair.Add(key, value);
            else
            {
                _keyValuePair.Remove(key);
                _keyValuePair.Add(key, value);
            }
        }

        public static T GetItemStatic<T>(string key)
        {
            object result;
            _keyValuePair.TryGetValue(key, out result);

            return (T)result;
        }
    }
}

