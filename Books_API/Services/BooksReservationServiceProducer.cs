using Books_API.Dto;
using Books_API.Exceptions;
using Books_API.Model;
using Books_API.Repository;
using Confluent.Kafka;
using System.Text.Json;

namespace Books_API.Services
{
    public class BooksReservationServiceProducer
    {
        private const string CONFIGS_KAFKA_SERVER = "Kafka:BootstrapServers";
        private const string KAFKA_TOPIC = "reservations_queue";
        private readonly IConfiguration _configuration;
        private readonly IProducer<Null, string> _producer;
        private readonly ILogger<BooksReservationServiceProducer> _logger;
        private readonly IBookRepository _bookRepository;

        public BooksReservationServiceProducer(IConfiguration configuration, ILogger<BooksReservationServiceProducer> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _configuration = configuration;

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = _configuration[CONFIGS_KAFKA_SERVER]
            };

            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        public async Task CreateReservation(string _id, ReservationReq reservationReq)
        {
            BookDataModel bookDataModel = await FindBookById(_id);
            
            await SetBookReserved(bookDataModel);

            ReservationMessage reservationMessage = new()
            {
                BookId = _id,
                StartReservationDate = reservationReq.StartReservationDate,
                EndReservationDate = reservationReq.EndReservationDate,
                UserDetails = reservationReq.UserDetails
            };

            var serializedMessage = JsonSerializer.Serialize(reservationMessage);

            var kafkaMessage = new Message<Null, string> { Value = serializedMessage };

            await _producer.ProduceAsync(KAFKA_TOPIC, kafkaMessage);

            _logger.LogInformation("Message sent!");
        }

        private async Task SetBookReserved(BookDataModel bookDataModel)
        {
            bookDataModel.Reserved = !bookDataModel.Reserved;

            await _bookRepository.UpdateBookAsync(bookDataModel);
        }

        private async Task<BookDataModel> FindBookById(string _id)
        {
            BookDataModel bookDataModel = await _bookRepository.GetBookByIdAsync(_id);

            if (bookDataModel.Reserved)
            {
                _logger.LogError("Book is unavailable");
                throw new BookUnavailableException("Book is unavailable");
            }

            return bookDataModel;
        }
    }
}
