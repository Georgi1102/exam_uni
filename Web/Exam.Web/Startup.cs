using Exam.Data;
using Exam.DataModels;
using Exam.Service;
using Exam.Service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Web
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
            services.AddDbContext<ExamDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IRentCarService, RentCarService>();
            services.AddTransient<IStatusService, StatusService>();  

            services.AddIdentity<ExamUser, IdentityRole<string>>()
                .AddEntityFrameworkStores<ExamDbContext>();

            services.AddRazorPages();

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

                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<ExamDbContext>())
                {
                    if (!dbContext.Roles.Any())
                    {
                        dbContext.Roles.Add(new IdentityRole
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });


                        dbContext.Roles.Add(new IdentityRole
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Client",
                            NormalizedName = "CLIENT",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });
                    }

                    if (!dbContext.Statuses.Any())
                    {
                        dbContext.Statuses.Add(new Status
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Изчакваща"         
                        });

                        dbContext.Statuses.Add(new Status
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Отменена"
                        });

                        dbContext.Statuses.Add(new Status
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Активна"
                        });

                        dbContext.Statuses.Add(new Status
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Използвана"
                        });

                        dbContext.Statuses.Add(new Status
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Просрочена",
                        });
                    }

                    dbContext.SaveChanges();
                }
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
