﻿using MongoDB.Driver;
using MVCWebApp.Models.User;

namespace MVCWebApp.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDBConnection");
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(configuration.GetSection("MongoDBConnection:DatabaseName").Value);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}
