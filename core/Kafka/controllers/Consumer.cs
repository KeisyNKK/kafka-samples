using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Public;
using Kafka.Public.Loggers;
using System.Text;

namespace Kafka.Hosts
{
    public class ConsumerHostedService : IHostedService
    {
        public readonly ILogger<ConsumerHostedService> _logger;
        private ClusterClient _cluster;

        public ConsumerHostedService(ILogger<ConsumerHostedService> logger)
        {
            _logger = logger;

            _cluster = new ClusterClient(new Configuration
                            {
                                Seeds = "localhost:29092"
                            }, new ConsoleLogger());
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cluster.ConsumeFromLatest("mytopic");

            Console.WriteLine("Recieved");
            _cluster.MessageReceived += record => 
            {
                var message = Encoding.UTF8.GetString((record.Value as byte[]));

                _logger.LogInformation($"Recieved: {message}");
            };

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            _cluster.Dispose();
            return Task.CompletedTask;
        }
    }
}