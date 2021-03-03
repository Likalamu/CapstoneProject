using Microsoft.EntityFrameworkCore;
using CapstoneTranslator.Models;

namespace CapstoneTranslator.Data
{
    public class LanguageDbContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<JobLanguage> JobLanguages { get; set; }


        public LanguageDbContext(DbContextOptions<LanguageDbContext> options)
            : base(options)
        {
        }

        public LanguageDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobLanguage>()
                .HasKey(j => new { j.JobId, j.LanguageId });
        }
    }
}
