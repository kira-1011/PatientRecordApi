using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    public MongoDB.Bson.ObjectId _id { get; set; }

    [BsonElement("email")]
    public string Email { get; set; } = "";

    [BsonElement("password")]
    public string Password { get; set; } = "";
}