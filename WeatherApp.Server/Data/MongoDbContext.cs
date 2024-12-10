namespace WeatherApp.Server.Data
{
    using MongoDB.Driver;
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }

    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}

