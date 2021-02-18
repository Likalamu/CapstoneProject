using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CapstoneTranslator.Data
{
    public class CapstoneTranslatorContext : IdentityDbContext<IdentityUser>
    {
        public CapstoneTranslatorContext(DbContextOptions<CapstoneTranslatorContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //modelBuilder.Entity<Users>().HasKey(et => new { et., et.TagId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
