using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redundancy.Extentions
{
    public static class Dependency
    {
        private static IServiceCollection services1;
        private static IConfiguration config;

        public static IServiceCollection DependencyServices
        {
            get
            {
                if (services1 == null)
                {
                    // 创建配置构建器
                    config = new ConfigurationBuilder()
                     .SetBasePath(AppContext.BaseDirectory) // 设置json文件所在的目录
                     .AddJsonFile("application.json", optional: true, reloadOnChange: true)
                     .Build();
                    services1 = new ServiceCollection();
                }
                return services1;
            }
        }

        public static IConfiguration Configuration
        {
            get
            {
                if (config == null)
                {
                    // 创建配置构建器
                    config = new ConfigurationBuilder()
                     .SetBasePath(AppContext.BaseDirectory) // 设置json文件所在的目录
                     .AddJsonFile("application.json", optional: true, reloadOnChange: true)
                     .Build();
                    services1 = new ServiceCollection();
                }
                return config;
            }
        }


        public static IServiceCollection Register(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection Register<IT,T>(this IServiceCollection services,IT IService, T Service) 
        {
            services.TryAdd(ServiceDescriptor.Singleton(typeof(IT), typeof(T)));
            return services;
        }

        public static IServiceCollection Register< T>(this IServiceCollection services, T Service)
        {
            services.TryAdd(ServiceDescriptor.Singleton(typeof(T)));
            return services;
        }

        public static IServiceCollection AddLoggings(this IServiceCollection services)
        {
            services.AddLogging();
            return services;
        }
        public static IServiceProvider BuilderProvider(this IServiceCollection services)
        {
            var provider = services.BuilderProvider();
            return provider;
        }
    }
}
