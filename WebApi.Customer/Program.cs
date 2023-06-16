using AutoMapper;
using Helper.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Models;
using Services.Interfaces;
using Services.MediatorPattern;
using Services.Services;
using Helper.Extensions;
using Microsoft.AspNetCore.Hosting.Server;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FFDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"), x => x.MigrationsAssembly("Models")));
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IMediator, ConcreteMediator>();
builder.Services.AddConsulConfig(builder.Configuration);
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseConsul("http://localhost:7261", "CustomerService");

app.MapControllers();

app.Run();
