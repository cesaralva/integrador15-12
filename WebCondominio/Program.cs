using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using WebApiCondominio.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CondominioContext>(
x => x.UseSqlServer(
builder.Configuration.GetConnectionString("defaultConnection")
)
);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "WEBVUE",
    builder =>
    {
        builder.WithOrigins("http://localhost:5028")

    .AllowAnyMethod()
    .AllowAnyHeader();
    });
});
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
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
