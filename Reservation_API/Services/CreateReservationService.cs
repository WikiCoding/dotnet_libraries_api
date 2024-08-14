using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Reservation_API.Exceptions;
using Reservation_API.Model;
using Reservation_API.Repository;
using System.Text.Json;

namespace Reservation_API.Services
{
    public class CreateReservationService : BackgroundService
    {
        private const string CONFIGS_KAFKA_SERVER = "Kafka:BootstrapServers";
        private const string KAFKA_TOPIC = "reservations_queue";
        private readonly ILogger<CreateReservationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly IServiceProvider _serviceProvider;

        public CreateReservationService(ILogger<CreateReservationService> logger, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceProvider = serviceProvider;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = configuration[CONFIGS_KAFKA_SERVER],
                GroupId = "ReservationsConsumerGroup",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        }

        public async Task CreateReservation(CancellationToken cancellationToken)
        {
            Reservation? reservation = ConsumeKafkaMessage(cancellationToken);

            if (reservation == null) throw new SerializerException("Error converting Json");

            using (var scope = _serviceProvider.CreateScope())
            {
                var reservationRepository = scope.ServiceProvider.GetRequiredService<IReservationRepository>();

                int rowsAffected = await reservationRepository.SaveReservationAsync(reservation);

                if (rowsAffected == 0)
                {
                    _logger.LogError("Error updating database");
                    throw new DbUpdateException();
                }

                _logger.LogInformation("Created Reservation Successfully");
            }
        }

        private Reservation? ConsumeKafkaMessage(CancellationToken cancellationToken)
        {
            Reservation? reservation;
            try
            {
                var consumeResult = _consumer.Consume(cancellationToken);
                var message = consumeResult.Message.Value;

                _logger.LogInformation("Received message: {message}", message);

                reservation = JsonSerializer.Deserialize<Reservation>(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error consuming message: {ex}", ex);
                throw;
            }

            return reservation;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(KAFKA_TOPIC);

            while (!stoppingToken.IsCancellationRequested) 
            {
                await CreateReservation(stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _consumer.Close();
        }
    }
}
