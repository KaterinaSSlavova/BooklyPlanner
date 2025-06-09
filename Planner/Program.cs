using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Planner.Mappers;

namespace Planner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(UserMapper).Assembly);

            builder.Services.AddDbContext<DBContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IReadingTaskRepository, ReadingTaskRepository>();
            builder.Services.AddScoped<IUserService, UserServices>();
            builder.Services.AddScoped<IReadingTaskService, ReadingTaskServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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
                endpoints.MapControllers(); 
            });


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=LogInForm}/{id?}");

            app.Run();
        }
    }
}
