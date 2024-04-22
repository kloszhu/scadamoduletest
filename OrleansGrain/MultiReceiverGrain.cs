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
    [ImplicitStreamSubscription("MuliDATA")]
    public class MultiReceiverGrain : Grain, IMultiReceiverGrain
    {
        private IStreamProvider streamProvider;
        private IAsyncStream<List<AddressValueSimplify>> stream;

        public override async Task OnActivateAsync()
        {

            // Create a GUID based on our GUID as a grain
            var guid = this.GetPrimaryKey();

            // Get one of the providers which we defined in config
            streamProvider = GetStreamProvider("SMSProvider");

            // Get the reference to a stream
            stream = streamProvider.GetStream<List<AddressValueSimplify>>(guid, "MuliDATA");
            // Set our OnNext method to the lambda which simply prints the data.
            // This doesn't make new subscriptions, because we are using implicit
            // subscriptions via [ImplicitStreamSubscription].
            await stream.SubscribeAsync<List<AddressValueSimplify>>(
                async (data, token) =>
                {
                    await Console.Out.WriteLineAsync($"进入消费时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                    await this.Print(data);
                    await Console.Out.WriteLineAsync($"结束消费时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                    await Task.CompletedTask;
                });
            await base.OnActivateAsync();
        }

        //public Task<int> GetAccount() { 
        //    return streamProvider..c
        //}

        private async Task Print(List<AddressValueSimplify> data)
        {

            await Console.Out.WriteLineAsync("First:" + data.First().Address + "--" + data.First().VL + "----" + data.First().Ts);
            await Console.Out.WriteLineAsync("Last:" + data.Last().Address + "--" + data.Last().VL + "----" + data.Last().Ts);
        }

    }
}
