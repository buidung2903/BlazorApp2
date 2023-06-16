using AutoMapper;
using Helper.Mapper;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Interfaces;
using Services.MediatorPattern;
using Services.Services;
using Helper.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:9998");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FFDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"), x => x.MigrationsAssembly("Models")));
builder.Services.AddTransient<IProductService, ProductService>();
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

app.UseConsul("https://localhost:9998");

app.MapControllers();

app.Run();

