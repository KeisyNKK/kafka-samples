using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Hosts
{
    public class ProducerHostedService : IHostedService
    {
        public readonly ILogger<ProducerHostedService> _logger;
        private IProducer<Null, string> _producer;

        public ProducerHostedService(ILogger<ProducerHostedService> logger)
        {
            _logger = logger;
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:29092"    
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            for(var i = 0; i < 100; i++){
                var value = $"Hello world {i}";
                _logger.LogInformation(value);
                await _producer.ProduceAsync("mytopic", new Message<Null, string>(){
                    Value = value
                }, cancellationToken);
            }

            _producer.Flush(TimeSpan.FromSeconds(10));
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            _producer.Dispose();
            return Task.CompletedTask;
        }
    }
}