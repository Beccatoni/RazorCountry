using Microsoft.EntityFrameworkCore;
using RazorCountry.Models;

namespace RazorCountry.Data;

public class CountryContext:DbContext
{
    public CountryContext(DbContextOptions<CountryContext> options) : base(options)
    {
        
    }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Continent> Continents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Country>().ToTable("Country");
      modelBuilder.Entity<Continent>().ToTable("Continent");
    }
}