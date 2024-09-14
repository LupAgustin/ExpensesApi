using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiExpenses.Models;

public partial class ApiExpensesContext : DbContext
{
    public ApiExpensesContext()
    {
    }

    public ApiExpensesContext(DbContextOptions<ApiExpensesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07619AB663");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Expenses__3214EC07EFDA5AEA");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expenses_Categories");

            entity.HasOne(d => d.User).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expenses_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07813CE4E3");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(10);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
