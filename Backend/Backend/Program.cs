using Backend.Models;
using Backend.Repositories;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Backend.Services.Interfaces;
using Backend.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Validations
builder.Services.AddSingleton<IValidator<Customer>, CustomerDetailsValidator>();

//Database
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();

//Business Services
builder.Services.AddSingleton<ICustomerService, CustomerService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature?.Error;


    var result = JsonConvert.SerializeObject(new { error = $"An unexpected error has occurred:{exception?.Message}" });
    context.Response.ContentType = MediaTypeNames.Application.Json;
    await context.Response.WriteAsync(result);
}));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
