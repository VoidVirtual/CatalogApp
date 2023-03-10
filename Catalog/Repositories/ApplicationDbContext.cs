using System;
using Microsoft.EntityFrameworkCore;
using Catalog.Models;
namespace Catalog.Repositories;

public class ApplicationDbContext: DbContext
{
    public DbSet<CatalogItem> CatalogItems {get; set;}

    public DbSet<Category> Categories {get; set;}

    public DbSet<Product> Products {get; set;}

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatalogItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                  .IsRequired(true);
            entity.HasOne(e => e.Parent)
                  .WithMany(e => e.Children)
                  .HasForeignKey(e => e.ParentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Product>(entity =>
        {    
            entity.Property(e => e.Price)
                  .IsRequired(true);
        });

        modelBuilder.Entity<Category>().HasData(new Category { Name = "All"});    
        base.OnModelCreating(modelBuilder);
    }
   
}