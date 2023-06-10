using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Test.Grains;

namespace OrleansSiloHost
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            return new HostBuilder()
                .UseOrleans(builder =>
                {
                    builder
                        .UseLocalhostClustering()
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "HelloWorldApp";
                        })
                        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
                        .UseMongoDBClient("mongodb://114.132.242.30:27017")
                        .AddLogStorageBasedLogConsistencyProviderAsDefault()
                        .AddMongoDBGrainStorage("PubSubStore", op =>
                        {
                            op.CollectionPrefix = "DispatcherOrelans";
                            op.DatabaseName = "Grain";
                        }). 
                        AddMongoDBGrainStorageAsDefault(optionsBuilder =>
                        {
                            optionsBuilder.Configure(op =>
                            {
                                op.CollectionPrefix = "DispatcherOrelans";

                                op.DatabaseName = "Grain";
                            });
                        })
                        .UseMongoDBClustering(op =>
                        {
                            op.CollectionPrefix = "DispatcherOrelans";
                            op.DatabaseName = "Memebership";
                        });
                })
                .ConfigureServices(services =>
                {
                    services.AddLogging(logging =>
                    {
                        logging.AddConsole();
                        logging.SetMinimumLevel(LogLevel.Information);
                    });
                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                })
                .RunConsoleAsync();
        }
    }
}
