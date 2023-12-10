using Microsoft.EntityFrameworkCore;

using NZwalks.API.Data;
using NZwalks.API.Mapping;
using NZwalks.API.Reposetories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksaConnectionString")));
builder.Services.AddAutoMapper(typeof(NZWalkingMappingcs));
builder.Services.AddScoped<IRegionRepository, SQLRegionRepositorys>();
builder.Services.AddScoped<IWalkReporitery, SQLWalkrepositery>();

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
