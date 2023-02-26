using Microsoft.AspNetCore.Mvc;
using PatientRecordApi.Models;
using PatientRecordApi.Services;

namespace PatientRecordApi.Controllers;

[ApiController]
[Route("api/patientrecords")]
public class PatientRecordController : ControllerBase
{
    private readonly PatientRecordServices _patientRecordService;

    public PatientRecordController(PatientRecordServices patientRecordService)
    {
        _patientRecordService = patientRecordService;
    }

    // Get request to retrieve all patient records
    [HttpGet]
    public async Task<List<PatientRecord>> Get()
    {
        return await _patientRecordService.GetAll();
    }

    // Get request to retrieve patient record by id
    [HttpGet("id/{id}")]
    public async Task<ActionResult<PatientRecord>> GetById(string id)
    {
        PatientRecord? patientRecord = null;

        try
        {
            patientRecord = await _patientRecordService.GetById(id);
        }
        catch (System.Exception)
        {
            return NotFound();
        }

        return Ok(patientRecord);
    }

    // Get request to retrieve patient records by full name
    [HttpGet("name/{name}")]
    public async Task<List<PatientRecord>> GetByName(string name)
    {
        return await _patientRecordService.GetByName(name);
    }

    // Post request to add a new patient record
    [HttpPost]
    public async Task<ActionResult> AddPatientRecord(PatientRecord patientRecord)
    {
        try
        {
            await _patientRecordService.Create(patientRecord);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }

        return Ok();
    }

    //Put request to update a patient record by id
    [HttpPut("id/{id}")]
    public async Task<ActionResult> UpdatePatientRecord(string id, PatientRecord patientRecord)
    {
        try
        {
            await _patientRecordService.Update(id, patientRecord);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }

        return Ok();
    }

    // Delete request to delete a patient record by id
    [HttpDelete("id/{id}")]
    public async Task DeletePatientRecord(string id)
    {
        await _patientRecordService.Delete(id);
    }
}