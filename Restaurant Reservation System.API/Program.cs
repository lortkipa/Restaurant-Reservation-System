
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Service;
using Restaurant_Reservation_System.Service.Interfaces;
using Restaurant_Reservation_System.Service.Mapping;
using System.Text;

namespace Restaurant_Reservation_System.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod());
            });

            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //    })

            // DB connection
            builder.Services.AddDbContext<RestaurantContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDBConnection"));
            });

            builder.Services.AddScoped<DbContext, RestaurantContext>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:Key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.ContainsKey("Token"))
                            {
                                context.Token = context.Request.Cookies["Token"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

            // Add repositories to the container
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

            // Add services to the container
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleUserService, RoleUserService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<IRestaurantService, RestaurantService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowAngular");

            app.UseHttpsRedirection();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
