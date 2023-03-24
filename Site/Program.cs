using Microsoft.AspNetCore.Authentication.Cookies;
using Site.Helpers;
using System.Net.Mail;
using System.Net;
using OfficeOpenXml;

namespace Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(options =>
            {
                //options.IdleTimeout = TimeSpan.FromSeconds(10); //tempo di durata della sessione
                //options.Cookie.HttpOnly = true;
                //options.Cookie.IsEssential = true;
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login"; //aggiungere url login per not authorized
                });


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

            app.UseAuthentication(); //serve per gestire l'authorize da mettere prima di authorization
            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            DatabaseHelper.ConnectionString = builder.Configuration.GetConnectionString("Ospedale");

            PathHelper.WebRootPath = app.Environment.ContentRootPath;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; //per attivare la licenza non commerciale

            EmailHelper.Email = builder.Configuration["email"];
            EmailHelper.Password = builder.Configuration["Password"];
            EmailHelper.PortSmtp = Convert.ToInt32(builder.Configuration["PortSmtp"]);
            EmailHelper.HostSmtp = builder.Configuration["HostSmtp"];
            EmailHelper.SmtpClient = new SmtpClient(EmailHelper.HostSmtp)
            {
                Port = EmailHelper.PortSmtp,
                Credentials = new NetworkCredential(EmailHelper.Email, EmailHelper.Password),
                EnableSsl = true,
            };

            app.Run();
        }
    }
}