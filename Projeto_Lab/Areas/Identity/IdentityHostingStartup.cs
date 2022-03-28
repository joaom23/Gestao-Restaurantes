using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projeto_Lab.Data;

[assembly: HostingStartup(typeof(Projeto_Lab.Areas.Identity.IdentityHostingStartup))]
namespace Projeto_Lab.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Projeto_LabContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Projeto_LabContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Projeto_LabContext>();
            });
        }
    }
}