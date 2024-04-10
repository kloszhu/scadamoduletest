using Orleans;
using OrleansIGrain;
using YSAI.Model.data;

namespace OrleansGrain
{
    public class Hello : Grain, IHello
    {
        Orleans.Streams.IAsyncStream<AddressValue> stream;
        public override Task OnActivateAsync()
        {
            // Pick a GUID for a chat room grain and chat room stream
            //  var guid = new Guid("some guid identifying the chat room");
            // Get one of the providers which we defined in our config
            var streamProvider = GetStreamProvider("SMSProvider");
            // Get the reference to a stream
            stream = streamProvider.GetStream<AddressValue>(Guid.NewGuid(), "RANDOMDATA");
            return base.OnActivateAsync();
        }
        public Task CreateStream(AddressValue address)
        {

            stream.OnNextAsync(address);
            return Task.CompletedTask;
        }


    }
}
