using PatientRecordApi.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace PatientRecordApi.Services;

public class PatientRecordServices
{
    private IMongoCollection<PatientRecord> _patientRecords;
    public PatientRecordServices(IOptions<PatientRecordDatabaseSettings> patientRecordDatabaseSettings)
    {
        //Get new MongoClient from the connection string
        var mongoClient = new MongoClient(patientRecordDatabaseSettings.Value.ConnectionString);

        //Get a database instance of the PatientRecordStore database
        var mongoDatabase = mongoClient.GetDatabase(patientRecordDatabaseSettings.Value.DatabaseName);

        //Get a collection instance of the "PatientRecords" collection
        _patientRecords = mongoDatabase.GetCollection<PatientRecord>(patientRecordDatabaseSettings.Value.CollectionName);
    }

    //CRUD operations

    // Get all patient records
    public async Task<List<PatientRecord>> GetAll()
    {
        return await _patientRecords.Find(_ => true).ToListAsync();
    }

    //Get a patient record by id
    public async Task<PatientRecord> GetById(string id)
    {
        return await _patientRecords.Find(record => record.Id == id).FirstOrDefaultAsync();
    }

    //Get patient records by name
    public async Task<List<PatientRecord>> GetByName(string name)
    {
        return await _patientRecords.Find(record => record.Name.IndexOf(name) > -1).ToListAsync();
    }

    // Add a new patient record
    public async Task Create(PatientRecord patientRecord)
    {
        await _patientRecords.InsertOneAsync(patientRecord);
    }

    // Update a patient record by id
    public async Task Update(string id, PatientRecord patientRecord)
    {
        await _patientRecords.ReplaceOneAsync(record => record.Id == id, patientRecord);
    }

    // Delete a patient record by id
    public async Task Delete(string id)
    {
        await _patientRecords.DeleteOneAsync(record => record.Id == id);
    }
}