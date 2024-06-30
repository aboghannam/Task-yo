
using Application.Interfaces;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Application.ClientData.Commands;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;
using Application;
using Microsoft.Extensions.Caching.Distributed;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserDataCommand).Assembly));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            builder.Services.AddControllers();
            builder.Services.AddValidatorsFromAssembly(typeof(CreateUserDataCommand).Assembly);
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
                options.InstanceName = "UserDataInstance";
            });


            builder.Services.AddScoped<IAppDbContext, AppDbContext>();
            builder.Services.AddMapster();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
