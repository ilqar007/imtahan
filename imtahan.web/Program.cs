using imtahan.BLL.DomainModel.IRepositories;
using imtahan.BLL.ServiceLayer.Services;
using imtahan.BLL.ServiceLayer.Services.Interfaces;
using imtahan.DAL.DataContext;
using imtahan.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace imtahan.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services
    .AddControllers().AddNewtonsoftJson()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
            builder.Services.AddSwaggerGen(c => c.EnableAnnotations());
            builder.Services.AddDbContext<ImtahanContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.EnableRetryOnFailure().CommandTimeout(60)));

            builder.Services.AddScoped(typeof(IRepo<>), typeof(RepoBase<>));
            builder.Services.AddScoped(typeof(IService<>), typeof(ServiceBase<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ImtahanContext>();

                dbContext.Database.EnsureCreated();
            }

            if (!app.Environment.IsDevelopment())
            {
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "My API V1");
            });

            //Add CORS Policy
            app.UseCors("AllowAll");

            //use attribute routing on controllers
            app.MapControllers();

            app.Run();
        }
    }
}