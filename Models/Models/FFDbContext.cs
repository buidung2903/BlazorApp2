using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models.EntityClass;

namespace Models.Models;

public partial class FFDbContext : DbContext
{
    public FFDbContext()
    {
    }

    public FFDbContext(DbContextOptions<FFDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=customerdb;Database=project_myself;User Id=sa;Password=Ad290399@;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=false");
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
