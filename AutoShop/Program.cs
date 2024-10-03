using DataAccess.Data;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using AutoShop.Services;
using Microsoft.AspNetCore.Identity;
using DataAccess.Entities;
using AutoShop.Helpers;

namespace AutoShop
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<CarContext>(
				options => options.UseSqlServer(builder.Configuration.GetConnectionString("CarContext")));
			
			builder.Services.AddDefaultIdentity<User>(
				options => options.SignIn.RequireConfirmedAccount = true
				)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<CarContext>();

			builder.Services.AddFluentValidationAutoValidation();
			builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddHttpContextAccessor();

			builder.Services.AddScoped<SessionData>();

			WebApplication app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				IServiceProvider serviceProvider = scope.ServiceProvider;

				RoleSeeder.SeedRoles(serviceProvider).Wait();
				RoleSeeder.SeedAdmin(serviceProvider).Wait();
			}

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
			app.UseSession();

			app.MapRazorPages();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
