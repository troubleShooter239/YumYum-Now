// using MongoDB.Driver;
// using MVCWebApp.Models.User;

// namespace MVCWebApp.Data;

// // Represents the MongoDB context for interacting with the database.
// public class MongoDbContext
// {
//     private readonly IMongoDatabase _database;
//     // Initializes a new instance of the <see cref="MongoDbContext"/> class.
//     public MongoDbContext(IConfiguration configuration)
//     {
//         try
//         {
//             // Retrieve the MongoDB connection string from the configuration.
//             var connectionString = configuration.GetConnectionString("ConnectionString");
//             // Create a new MongoClient using the retrieved connection string.
//             var client = new MongoClient(connectionString);

//             // Retrieve the DatabaseName from the ConnectionStrings section
//             var databaseName = configuration.GetSection("ConnectionStrings:DatabaseName").Value;

//             if (string.IsNullOrEmpty(databaseName))
//             {
//                 throw new InvalidOperationException("Database name is missing or invalid in configuration.");
//             }
//             // Get the MongoDB database instance based on the configuration.
//             _database = client.GetDatabase(databaseName);
//         }
//         catch (Exception ex)
//         {
//             throw new Exception("Error initializing MongoDB connection.", ex);
//         }
//     }

//     // Gets the MongoDB collection for user documents.
//     public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
// }
