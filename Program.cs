using PatientRecordApi.Models;
using PatientRecordApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<PatientRecordDatabaseSettings>(
    builder.Configuration.GetSection("PatientRecordDatabase")
);


//The PatientRecordService class is registered with DI to support constructor injection in consuming classes
builder.Services.AddSingleton<PatientRecordServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
