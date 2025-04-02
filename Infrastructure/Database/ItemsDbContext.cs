using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class ItemsDbContext : DbContext
{
	public DbSet<Item> Items { get; set; }
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseInMemoryDatabase("Orders");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Item>()
			.HasKey(x => x.Id);
		modelBuilder.Entity<Item>()
			.Property(x => x.Name)
			.HasMaxLength(50);
		modelBuilder.Entity<Item>()
			.Property(x => x.Price)
			.HasPrecision(10, 2);
		modelBuilder.Entity<Item>()
			.Property(x => x.Description)
			.HasMaxLength(150);
	}
}