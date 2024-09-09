using EmployeeProject.Models;
using EmployeeProject.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//add servicees 
builder.Services.AddDbContext<SampleDbContext>(
       options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//add services
builder.Services.AddScoped<EmployeeService,EmployeeService>();
builder.Services.AddScoped<Companyservices, Companyservices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(options=>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.MapControllers();

app.Run();
