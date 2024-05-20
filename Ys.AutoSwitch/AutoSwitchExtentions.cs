using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.AutoSwitch.netmq;
using YS.Models.Auto.Config;

namespace Ys.AutoSwitch
{
    public static class AutoSwitchExtentions
    {
        public static IServiceCollection AddAutoSwitch(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<AutoSwitchJsonConfig>(configuration.GetSection("AutoSwitch"));
            services.AddSingleton<NetManager>();
            services.AddSingleton<AutoSwitchManager>();
            return services;
        }
    }
}
