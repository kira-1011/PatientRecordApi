using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonElement("email")]
    public string Email { get; set; } = "";

    [BsonElement("password")]
    public string Password { get; set; } = "";
}