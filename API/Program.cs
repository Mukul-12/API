/*using Serilog;*/

using API;
using API.Custom_Logging;
using API.Data;
using API.Repository;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("mycon"));
});

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddSingleton<ICustomLogger, CustomLogger>();*/

/*Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
.WriteTo.File("log/serilog.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();
builder.Host.UseSerilog();*/

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
