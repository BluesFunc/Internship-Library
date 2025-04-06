using Application;
using Infrastructure;
using Infrastructure.Extension;
using Microsoft.AspNetCore.Authorization;

using WebAPI.Extensions;
using WebAPI.Middlewares;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddOpenApi()
            .AddApplication()
            .AddInfrastructure()
            .AddHttpLogging(o => {});
        


        var app = builder.Build();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();

       
        app.Run();
    }
}