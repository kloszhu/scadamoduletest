using Orleans;
using Orleans.Streams;
using OrleansIGrain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSAI.Model.data;

namespace OrleansGrain
{
    /// <summary>
    /// 自动激活Grain
    /// </summary>
    [ImplicitStreamSubscription("RANDOMDATA")]
    public class ReceiverGrain : Grain, IRandomReceiver
    {
        IAsyncStream<AddressValue> stream;
        public override async Task OnActivateAsync()
        {

            // Create a GUID based on our GUID as a grain
            var guid = this.GetPrimaryKey();

            // Get one of the providers which we defined in config
            var streamProvider = GetStreamProvider("SMSProvider");

            // Get the reference to a stream
           stream = streamProvider.GetStream<AddressValue>(guid, "RANDOMDATA");

            // Set our OnNext method to the lambda which simply prints the data.
            // This doesn't make new subscriptions, because we are using implicit
            // subscriptions via [ImplicitStreamSubscription].
            await stream.SubscribeAsync<AddressValue>(
               async (data, token) =>
               {
                   //Console.Out.WriteLine("B:" + data.AddressAnotherName + "---" + data.AddressName + "-" + data.Value);
                   await Task.CompletedTask;
               });
            await base.OnActivateAsync();
        }

        public override Task OnDeactivateAsync()
        {
            return base.OnDeactivateAsync();
        }

    }
}
