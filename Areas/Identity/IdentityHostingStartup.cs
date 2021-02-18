using System;
using CapstoneTranslator.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CapstoneTranslator.Areas.Identity.IdentityHostingStartup))]
namespace CapstoneTranslator.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CapstoneTranslatorContext>(options =>
                    options.UseMySql(
                        context.Configuration.GetConnectionString("CapstoneTranslatorContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<CapstoneTranslatorContext>();
            });
        }
    }
}