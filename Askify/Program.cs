using Askify.Models;
using Askify.Repositories;
using Askify.Repositories.Context;
using Askify.Repositories.IRepositories;
using Askify.Services;
using Askify.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Core;
using System.Runtime.CompilerServices;

namespace Askify
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("default");
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddIdentity<AppUser, AppRole>
                (options =>
                {
                    //options.Password.RequireDigit = true;
                    //options.Password.RequireUppercase = true;
                    //options.Password.RequiredUniqueChars = 1;
                })
                .AddEntityFrameworkStores<ApplicationContext>();
            builder.Services.AddScoped<IEnduserService, EnduserService>();
            builder.Services.AddScoped<IEnduserRepository, EnduserRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
            builder.Services.AddScoped<IAnswerService, AnswerService>();
            builder.Services.AddScoped<ITimelineRepository, TimelineRepository>();
            builder.Services.AddScoped<ITimelineService, TimelineService>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IUserAnswerLikesRepository, UserAnswerLikesRepository>();
            builder.Services.AddScoped<IUserAnswerLikesService, UserAnswerLikesService>();

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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=enduser}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                await InitializeRolesAndUsers(roleManager, userManager);
            }
            app.Run();
        }
        private static async Task InitializeRoles(RoleManager<AppRole> roleManager)
        {
            string[] roleNames = { "Admin" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Create the role if it doesn't exist
                    var role = new AppRole { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
            }
        }
        private static async Task InitializeRolesAndUsers(
        RoleManager<AppRole> roleManager,
        UserManager<AppUser> userManager)
        {
            // Initialize roles
            await InitializeRoles(roleManager);

            //this is iquerable and i want it as int
            // Create an admin user
            var adminEmail = "admin@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = "omarnaru",
                    Email = adminEmail,
                    EndUserId = 3
                };
                await userManager.CreateAsync(adminUser, "OmarNaruSubs#2002");
            }

            // Assign the admin user to the "Admin" role
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
