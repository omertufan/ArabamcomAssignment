using ArabamcomAssignment.Entities;
using ArabamcomAssignment.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ArabamcomAssignment
{


    public class ConsumerWorkerService : BackgroundService
    {
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        public ConsumerWorkerService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
      
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(_configuration.GetValue<string>("RabbitMQSettings:HostAddress"))
            };
            //{
            //    HostName = "rabbitmq",
            //    UserName = "guest",
            //    Password = "guest",
            //    Port = 5672,
            //    RequestedConnectionTimeout = TimeSpan.FromMilliseconds(3000), // milliseconds
            //};
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "advertVisit",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            return base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var json = JsonSerializer.Deserialize<AdvertVisit>(message);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var advertService = scope.ServiceProvider.GetService<IAdvertRepository>();
                    await advertService.AddAdvertVisit(json);
                }


            };

            _channel.BasicConsume(
              queue: "advertVisit",
              autoAck: true,
              consumer: consumer);

            await Task.CompletedTask;
        }
    }
}
