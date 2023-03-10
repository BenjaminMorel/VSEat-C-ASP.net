using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDeliveryStaffManager, DeliveryStaffManager>();
            services.AddScoped<IDeliveryStaffDB, DeliveryStaffDB>(); 

            services.AddScoped<ILocationManager, LocationManager>();
            services.AddScoped<ILocationDB, LocationDB>();

            services.AddScoped<ILoginManager, LoginManager>();
            services.AddScoped<ILoginDB, LoginDB>();

            services.AddScoped<IRestaurantTypeManager, RestaurantTypeManager>();
            services.AddScoped<IRestaurantTypeDB, RestaurantTypeDB>();

            services.AddScoped<ILoginTypeManager, LoginTypeManager>();
            services.AddScoped<ILoginTypeDB, LoginTypeDB>();

            services.AddScoped<IOrderDetailsManager, OrderDetailsManager>();
            services.AddScoped<IOrderDetailsDB, OrderDetailsDB>();

            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IOrderDB, OrderDB>();

            services.AddScoped<IOrderStatusManager, OrderStatusManager>();
            services.AddScoped<IOrderStatusDB, OrderStatusDB>();

            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IProductDB, ProductDB>();

            services.AddScoped<IRegionManager, RegionManager>();
            services.AddScoped<IRegionDB, RegionDB>();

            services.AddScoped<IRestaurantManager, RestaurantManager>();
            services.AddScoped<IRestaurantDB, RestaurantDB>();

            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUserDB, UserDB>();

            services.AddScoped<ICartDetailsManager, CartDetailsManager>();
            services.AddScoped<ICartDetailsDB, CartDetailsDB>(); 

            services.AddScoped<IDeliveryStaffTypeManager, DeliveryStaffTypeManager>();
            services.AddScoped<IDeliveryStaffTypeDB, DeliveryStaffTypeDB>();

            services.AddScoped<IReviewManager, ReviewManager>();
            services.AddScoped<IReviewDB, ReviewDB>(); 


            services.AddSession();
            


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Restaurant}/{action=Index}/{id?}");
            });
        }
    }
}
