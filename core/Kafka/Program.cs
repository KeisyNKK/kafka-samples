using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Kafka.Hosts;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, collection) =>
                {
                    collection.AddHostedService<ProducerHostedService>();
                    collection.AddHostedService<ConsumerHostedService>();
                });
    }
}
