using Microsoft.Extensions.Caching.Memory;

public class CacheHelper
{
    private static CacheHelper _helper;
    private readonly static object _obj = new object();
    private readonly IMemoryCache _cache;
    private CacheHelper()
    {
        _cache = new MemoryCache(new MemoryCacheOptions());
    }
    /// <summary>
    /// 实例
    /// </summary>
    public static CacheHelper Instance
    {
        get
        {
            if (_helper == null)
            {
                lock (_obj)
                {
                    if (_helper == null)
                    {
                        _helper = new CacheHelper();
                    }
                }
            }
            return _helper;
        }
    }
    /// <summary>
    /// 取得缓存数据
    /// </summary>
    /// <param name="key">关键字</param>
    /// <returns></returns>
    public string Get(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return default;
        return _cache.Get<string>(key);
    }
    /// <summary>
    /// 设置缓存(滑动过期:超过一段时间不访问就会过期,一直访问就一直不过期)
    /// </summary>
    /// <param name="key">关键字</param>
    /// <param name="value">缓存值</param>
    public void Set_SlidingExpire<T>(string key, T value, TimeSpan span)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));
        T v;
        if (_cache.TryGetValue(key, out v))
            _cache.Remove(key);
        _cache.Set(key, value, new MemoryCacheEntryOptions()
        {
            SlidingExpiration = span
        });
    }
    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key">关键字</param>
    /// <param name="value">缓存值</param>
    public void Set(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));
        string v;
        if (_cache.TryGetValue(key, out v))
            _cache.Remove(key);
        _cache.Set(key, value);
    }
    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key">关键字</param>
    /// <param name="value">缓存值</param>
    public void Set<T>(string key, T value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));
        string v;
        if (_cache.TryGetValue(key, out v))
            _cache.Remove(key);
        _cache.Set(key, value);
    }
    /// <summary>
    /// 取得缓存数据
    /// </summary>
    /// <param name="key">关键字</param>
    /// <returns></returns>
    public T Get<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return default;
        return _cache.Get<T>(key);
    }
}