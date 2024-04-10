using Orleans;
using Orleans.Streams;
using OrleansIGrain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSAI.Model.data;

namespace OrleansGrain
{
    [ImplicitStreamSubscription("MuliDATA")]
    public class MultiReceiverGrain : Grain, IMultiReceiverGrain
    {

        public override async Task OnActivateAsync()
        {

            // Create a GUID based on our GUID as a grain
            var guid = this.GetPrimaryKey();

            // Get one of the providers which we defined in config
            var streamProvider = GetStreamProvider("SMSProvider");

            // Get the reference to a stream
            var stream = streamProvider.GetStream<List<AddressValueSimplify>>(guid, "MuliDATA");

            // Set our OnNext method to the lambda which simply prints the data.
            // This doesn't make new subscriptions, because we are using implicit
            // subscriptions via [ImplicitStreamSubscription].
            await stream.SubscribeAsync<List<AddressValueSimplify>>(
                async (data, token) =>
                {
                    this.Print(data);
                    await Task.CompletedTask;
                });
            await base.OnActivateAsync();
        }


        private async Task Print(List<AddressValueSimplify> data)
        {
            await Task.Delay(1000);
            await Console.Out.WriteLineAsync("EndUP" + data.First().Address +"--"+data.First().Id);
        }

    }
}
