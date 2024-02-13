using DevFreela.Payments.API.Models;
using DevFreela.Payments.API.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Payments.API.Consumers
{
    public class ProcessPaymentConsumer : BackgroundService
    {
        private const string QUEUE = "Payments";
        private const string PAYMENT_APPROVED_QUEUE = "PaymentsApproved";
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public ProcessPaymentConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            // Creating a connection
            _connection = factory.CreateConnection();
            // Creating a channel
            _channel = _connection.CreateModel();

            // If the queues do not exit, then it is going to be created
            _channel.QueueDeclare(queue: QUEUE,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            _channel.QueueDeclare(queue: PAYMENT_APPROVED_QUEUE,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);

            // What to do with the received message
            consumer.Received += (send, eventArgs) =>
            {
                byte[] byteArray = eventArgs.Body.ToArray();
                string paymentInfoJson = Encoding.UTF8.GetString(byteArray);
                PaymentInfoInputModel paymentInfo = JsonSerializer.Deserialize<PaymentInfoInputModel>(paymentInfoJson);

                ProcessPayment(paymentInfo);

                PaymentApprovedIntegrationEvent paymentApproved = new PaymentApprovedIntegrationEvent(paymentInfo.IdProject);
                string paymentApprovedJson = JsonSerializer.Serialize(paymentApproved);
                byte[] paymentApprocedBytes = Encoding.UTF8.GetBytes(paymentApprovedJson);

                // Just for business rule we are sending a message to change the status
                // of the project later after the payment be processed
                _channel.BasicPublish(exchange: "",
                                      routingKey: PAYMENT_APPROVED_QUEUE,
                                      basicProperties: null,
                                      body: paymentApprocedBytes);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        public void ProcessPayment(PaymentInfoInputModel paymentInfo)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IPaymentService paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
                paymentService.Process(paymentInfo);
            }
        }
    }
}
