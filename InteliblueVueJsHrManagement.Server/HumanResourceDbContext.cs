// This file is provided freely for use, modification, and distribution without restriction. 
// No specific license applies, and you are free to share, modify, and redistribute this file as needed.

using InteliblueVueJsHrManagement.Server.Models.DbSets;
using Microsoft.EntityFrameworkCore;
namespace InteliblueVueJsHrManagement.Server;

public class HumanResourceDbContext : DbContext
{

    public HumanResourceDbContext(DbContextOptions<HumanResourceDbContext> options) : base(options)
    {
    }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Department>().Property(p => p.Name).HasColumnType("varchar").HasMaxLength(128);
        modelBuilder.Entity<Department>().HasIndex(p => p.Name).IsUnique();

        // Manually add some departments
        var departmentId = -1;
        modelBuilder.Entity<Department>().HasData(
               new Department() { Id = departmentId--, Name = "Human Resources" },
               new Department() { Id = departmentId--, Name = "Finance" },
               new Department() { Id = departmentId--, Name = "IT" },
               new Department() { Id = departmentId--, Name = "Marketing" },
               new Department() { Id = departmentId--, Name = "Sales" }

        );

        modelBuilder.Entity<Employee>().Property(p => p.FirstName).HasColumnType("varchar").HasMaxLength(32);
        modelBuilder.Entity<Employee>().Property(p => p.LastName).HasColumnType("varchar").HasMaxLength(32);
        modelBuilder.Entity<Employee>().Property(p => p.Email).HasColumnType("varchar").HasMaxLength(64);
        modelBuilder.Entity<Employee>().Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(16);
    }
}
