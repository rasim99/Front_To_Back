using FrontToBack.DAL;
using FrontToBack.Sevices;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack
{
    public  static class ServiceRegistration
    {
        public static void ServicesRegister(this IServiceCollection services,IConfiguration _config)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ISum, SumService>();
            services.AddScoped<AccountService>(a => new AccountService());
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });
            services.AddHttpContextAccessor();
            services.AddScoped<IBasketService , BasketService > ();
        }
    }
}
