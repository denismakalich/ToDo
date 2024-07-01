using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Repositories;
using ToDo.Inftrastructure.Context;
using ToDo.Inftrastructure.Repositories;
using ToDo.WebAPI.Exceptions;
using ToDo.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        b =>
        {
            b.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();
builder.Services.AddControllersWithJsonOptions();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<ITenantFactory, TenantFactoryRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthorizationAndAuthentication();
builder.Services.AddSerilog();

var app = builder.Build();
app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseExceptionHandler();
app.UseAuthorizationAndAuthentication();

if (builder.Configuration.GetValue<bool>("AutoMigrations"))
{
    using var scope = app.Services.CreateScope();
    var scopedServices = scope.ServiceProvider;
    var context = scopedServices.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.Run();