using Microsoft.EntityFrameworkCore;

using App.Models;

namespace App.Context
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>(r =>
            {
                r.HasKey(e => e.Id);
                r.Property(e => e.Id).ValueGeneratedOnAdd();
                r.Property(e => e.Id).IsRequired();
                r.Property(e => e.Status).IsRequired();
                r.Property(e => e.StartAddress).IsRequired();
                r.Property(e => e.EndAddress).IsRequired();
                r.Property(e => e.StartDate).IsRequired();
                r.Property(e => e.EndDate).IsRequired();
                r.HasOne(e=> e.User).WithMany(x=>x.Records);
            }
            );
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(e => e.Id);
                u.Property(e => e.Id).ValueGeneratedOnAdd();
                u.Property(e => e.Id).IsRequired();
                u.Property(e => e.Email).IsRequired();
                u.Property(e => e.Name).IsRequired();
            }
                       );
            modelBuilder.Entity<Code>(c =>
            {
                c.HasKey(e => e.Id);
            }
                        );

        }
        public DbSet<Record> Records { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Code> Codes { get; set; }
    }
}