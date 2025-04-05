using Application;
using Infrastructure;
using Infrastructure.Extension;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddApplication()
            .AddInfrastructure();
            
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.MapControllers();

        app.MapGet("/hello", [Authorize]() => "Hello world!");
        app.Run();
    }
}