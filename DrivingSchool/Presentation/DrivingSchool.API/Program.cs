using DrivingSchool.API.Middleware;
using DrivingSchool.Application.Extensions;
using DrivingSchool.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterDatabase(builder.Configuration);
builder.Services.RegisterMediator(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.MigrateDatabase();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
