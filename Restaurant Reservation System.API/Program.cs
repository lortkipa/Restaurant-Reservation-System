
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System.Data;

namespace Restaurant_Reservation_System.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DB connection
            builder.Services.AddDbContext<RestaurantContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDBConnection"));
            });

            builder.Services.AddScoped<DbContext, RestaurantContext>();

            builder.Services.AddControllers();

            // Add repositories to the container

            // Add services to the container

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
