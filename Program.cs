using PatientRecordApi.Models;
using PatientRecordApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
CookieOptions cookieOps = new CookieOptions();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

builder.Services.AddMvc();

builder.Services.Configure<PatientRecordDatabaseSettings>(
    builder.Configuration.GetSection("PatientRecordDatabase")
);

builder.Services.Configure<UserDatabaseSettings>(
    builder.Configuration.GetSection("UserDatabase")
);


//The PatientRecordService class is registered with DI to support constructor injection in consuming classes
builder.Services.AddSingleton<PatientRecordServices>();
builder.Services.AddSingleton<UserServices>();

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


app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

CookiePolicyOptions cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax
};

app.UseCookiePolicy(cookiePolicyOptions);

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
