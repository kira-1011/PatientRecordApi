using PatientRecordApi.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace PatientRecordApi.Services;

public class UserServices
{
    private IMongoCollection<User> _users;
    public UserServices(IOptions<UserDatabaseSettings> userDatabaseSettings)
    {
        //Get new MongoClient from the connection string
        var mongoClient = new MongoClient(userDatabaseSettings.Value.ConnectionString);

        //Get a database instance of the PatientRecordStore database
        var mongoDatabase = mongoClient.GetDatabase(userDatabaseSettings.Value.DatabaseName);

        //Get a collection instance of the "PatientRecords" collection
        _users = mongoDatabase.GetCollection<User>(userDatabaseSettings.Value.CollectionName);
    }

    public async Task<bool> Login(User user)
    {
        var result = await _users.Find(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();

        if (result == null)
        {
            return false;
        }
        return true;
    }

    public async Task SignUp(User user)
    {
        await _users.InsertOneAsync(user);
    }
}