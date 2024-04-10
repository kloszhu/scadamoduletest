using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;

public static class StackExchangeRedisOperate 
{
    private static  ConnectionMultiplexer _rediConnection;
    private static  ISubscriber _subscriber;
    private static  IDatabase _database;

    public static ConnectionMultiplexer ConnectionMultiplexerInstance { get { return _rediConnection; } }

    public static void Init(string configuration= "192.168.2.221,password=tEYNdk6yTcfm6HmY,defaultDatabase=12")
    {
        _rediConnection = ConnectionMultiplexer.Connect(configuration);
        _subscriber = _rediConnection.GetSubscriber();
        _database = _rediConnection.GetDatabase();
    }

    public static async Task AddString(string key, dynamic value, TimeSpan? expiry)
    {
     
        await _database.StringSetAsync(key, value, expiry: expiry);
        await Console.Out.WriteLineAsync(key);
    }

    public static async Task<string> GetString(string key)
    {
        if (key == "2222")
        {
            return "qwerdf";
        }
        return await _database.StringGetAsync(key);
    }

    public static async Task Publish<T>(string channelName, T message)
    {
        try
        {
            await _subscriber.PublishAsync(new RedisChannel(Encoding.UTF8.GetBytes(channelName), RedisChannel.PatternMode.Auto), JsonConvert.SerializeObject(message));
        }
        catch
        {
            throw;
        }
    }

    public static async Task Subscribe(string channelName, Action<RedisValue> doAction)
    {
        await _subscriber.SubscribeAsync(new RedisChannel(Encoding.UTF8.GetBytes(channelName), RedisChannel.PatternMode.Auto), (rChannel, rValue) =>
        {
            // 消费
            doAction(rValue);
        });
    }

    public static string GetValue(string key)
    {
        return _database.StringGet(key);
    }
}