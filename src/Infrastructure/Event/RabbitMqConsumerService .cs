using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.Infrastructure.Event;


public class RabbitMqConsumerService : IRabbitMqConsumerService
{

    private readonly string _hostname;
    private readonly string _queueName;
    private IModel? _channel;
    private IConnection? _connection;
    private readonly ILogger<IRabbitMqConsumerService> _logger;
    public RabbitMqConsumerService(string hostname, string queueName, ILogger<IRabbitMqConsumerService> logger)
    {
        _hostname = hostname;
        _queueName = queueName;
        _logger = logger;
    }


    public void StartAsync()
    {
        var factory = new ConnectionFactory() { HostName = _hostname };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation("Received Message: {Message}", message);
            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(queue: _queueName,
                             autoAck: false,
                             consumer: consumer);

        _logger.LogInformation("Waiting for messages.");

        
    }

    public void StopAsync()
    {
        _channel?.Close();
        _connection?.Close();
        _logger.LogInformation("RabbitMQ Consumer Service is stopping.");
        

    }
}
