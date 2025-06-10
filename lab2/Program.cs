using lab2.Models;
using lab2.Utils;
using Microsoft.EntityFrameworkCore;

namespace lab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            


            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 4, 2))));

            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor(); // для доступу до HttpContext
            builder.Services.AddScoped<UserContext>();

            var app = builder.Build();
            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
