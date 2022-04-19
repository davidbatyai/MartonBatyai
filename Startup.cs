using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MindenMancs.Data;
using MindenMancs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindenMancs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private async Task JogosultsagokBeallitasa(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var vanUser = await RoleManager.RoleExistsAsync("Shelter");
            if (!vanUser)
            {
                IdentityRole szerepkor = new IdentityRole("Shelter");
                await RoleManager.CreateAsync(szerepkor);
            }
            var vanAdmin = await RoleManager.RoleExistsAsync("Admin");
            if (!vanAdmin)
            {
                IdentityRole szerepkor = new IdentityRole("Admin");
                await RoleManager.CreateAsync(szerepkor);
            }
            var vanSuperAdmin = await RoleManager.RoleExistsAsync("SuperAdmin");
            if (!vanSuperAdmin)
            {
                IdentityRole szerepkor = new IdentityRole("SuperAdmin");
                await RoleManager.CreateAsync(szerepkor);
            }

            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser Sadmin = await userManager.FindByNameAsync("davidbatyai@gmail.com");
            if (Sadmin == null)
            {
                var felhasznalo = new IdentityUser { UserName = "davidbatyai@gmail.com", Email = "davidbatyai@gmail.com", EmailConfirmed = true, LockoutEnabled = false };
                var letre = await userManager.CreateAsync(felhasznalo, "@D1n");
                if (letre.Succeeded)
                {
                    await userManager.AddToRoleAsync(felhasznalo, "SuperAdmin");
                }
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddAuthentication().AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                Configuration.GetSection("Authentication:Google");

                options.ClientId = "507827530877-58o5e78n1vdh2b2oe410r94sgosa73lj.apps.googleusercontent.com";
                options.ClientSecret = "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";
}
);

            services.AddTransient<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
