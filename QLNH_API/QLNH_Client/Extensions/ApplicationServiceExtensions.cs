using Microsoft.EntityFrameworkCore;
using QLNH_Client.Data;
using QLNH_Client.Repositories;
using QLNH_Client.Services;

namespace QLNH_Client.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {
            #region Connect to SQL Server
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Add Cors
            services.AddCors(options => options.AddPolicy(name: "QLNHOrigins",
                policy =>
                {
                    policy.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                }));
            #endregion

            #region AddScope DI
            services.AddScoped<ITokenSerivce, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemImageRepository, ItemImageRepository>();
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IGuestTableRepository, GuestTableRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IPriceRepository, PriceRepository>();

            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(Program).Assembly);
            #endregion

            #region Add NewtonSoftJson
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            #endregion

            return services;
        }
    }
}
