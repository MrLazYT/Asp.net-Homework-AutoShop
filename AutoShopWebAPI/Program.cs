using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Sevices;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoShopWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connection = builder.Configuration.GetConnectionString("CarContext")!;

            builder.Services.AddDbContext<CarContext>(options =>
            {
                options.UseSqlServer(connection);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddScoped<CarService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddAutoMapper(typeof(MapperProfile));

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