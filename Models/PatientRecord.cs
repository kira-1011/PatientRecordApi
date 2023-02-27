using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PatientRecordApi.Models;

public class PatientRecord
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("age")]
    public uint Age { get; set; }

    [BsonElement("address")]
    public string Address { get; set; }

    [BsonElement("phoneNumber")]
    public string PhoneNumber { get; set; }
    public PatientRecord(string id, string name, uint age, string address, string phoneNumber)
    {
        this.Id = id;
        this.Name = name;
        this.Age = age;
        this.Address = address;
        this.PhoneNumber = phoneNumber;
    }
}