using EmployeeTaskManagement.Api.Middleware;
using EmployeeTaskManagement.Application.Interfaces;
using EmployeeTaskManagement.Application.Profiles;
using EmployeeTaskManagement.Application.Services;
using EmployeeTaskManagement.Core.Interfaces;
using EmployeeTaskManagement.Infrastructure.Data;
using EmployeeTaskManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;


var builder = WebApplication.CreateBuilder(args);

Batteries.Init();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IEmployeeTaskService, EmployeeTaskService>();
builder.Services.AddScoped<IEmployeeReportService, EmployeeReportService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsEnvironment("Local"))
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        DataSeeder.Reseed(services);
        DataSeeder.Seed(services);
    }
    catch (Exception ex)
    {
        throw;
    }
}


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
