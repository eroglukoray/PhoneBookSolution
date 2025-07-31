using Microsoft.EntityFrameworkCore;
using ContactService.Models;

namespace ContactService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<ContactInfo> ContactInfos => Set<ContactInfo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>().HasKey(p => p.Id);
        modelBuilder.Entity<ContactInfo>().HasKey(c => c.Id);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.ContactInfos)
            .WithOne(c => c.Person)
            .HasForeignKey(c => c.PersonId);
    }
}
