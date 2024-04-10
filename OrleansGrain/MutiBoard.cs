using Orleans;
using OrleansIGrain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSAI.Model.data;

namespace OrleansGrain
{
    public class MutiBoard: Grain, IMutiBoard
    {

        Orleans.Streams.IAsyncStream<List<AddressValueSimplify>> stream;
        public override Task OnActivateAsync()
        {
            // Pick a GUID for a chat room grain and chat room stream
            //  var guid = new Guid("some guid identifying the chat room");
            // Get one of the providers which we defined in our config
            var streamProvider = GetStreamProvider("SMSProvider");
            // Get the reference to a stream
            stream = streamProvider.GetStream<List<AddressValueSimplify>>(Guid.NewGuid(), "MuliDATA");
            return base.OnActivateAsync();
        }
        public Task CreateStream(List<AddressValueSimplify> address)
        {

            stream.OnNextAsync(address);
            return Task.CompletedTask;
        }
    }
}
