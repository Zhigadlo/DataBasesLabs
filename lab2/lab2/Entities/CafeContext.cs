using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab2.Entities;

public partial class CafeContext : DbContext
{
    public CafeContext()
    {
    }

    public CafeContext(DbContextOptions<CafeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dish> Dishes { get; set; } = null!;

    public virtual DbSet<Employee> Employees { get; set; } = null!;

    public virtual DbSet<EmployeeInformation> EmployeeInformations { get; set; } = null!;

    public virtual DbSet<Ingridient> Ingridients { get; set; } = null!;

    public virtual DbSet<IngridientWarehousesInformation> IngridientWarehousesInformations { get; set; } = null!;

    public virtual DbSet<IngridientsDish> IngridientsDishes { get; set; } = null!;

    public virtual DbSet<IngridientsWarehouse> IngridientsWarehouses { get; set; } = null!;

    public virtual DbSet<Order> Orders { get; set; } = null!;

    public virtual DbSet<OrderCost> OrderCosts { get; set; } = null!;

    public virtual DbSet<OrderDish> OrderDishes { get; set; } = null!;

    public virtual DbSet<Profession> Professions { get; set; } = null!;

    public virtual DbSet<Provider> Providers { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(DbConnection.Instance.GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dishes__3214EC072FB47970");

            entity.HasIndex(e => e.Name, "UQ__Dishes__737584F6AAF19777").IsUnique();

            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.CookingTime).HasColumnOrder(3);
            entity.Property(e => e.Cost).HasColumnOrder(2);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(1);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07111A9BE4");

            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.Age)
                .HasColumnOrder(4)
                .HasDefaultValueSql("((18))");
            entity.Property(e => e.Education)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnOrder(5);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(1);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(2);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(3);
            entity.Property(e => e.ProfessionId).HasColumnOrder(6);

            entity.HasOne(d => d.Profession).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ProfessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_To_Professions");
        });

        modelBuilder.Entity<EmployeeInformation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EmployeeInformations");

            entity.Property(e => e.Age).HasColumnOrder(3);
            entity.Property(e => e.Education)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnOrder(4);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(0);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(1);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(2);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(5);
        });

        modelBuilder.Entity<Ingridient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingridie__3214EC07981C0780");

            entity.HasIndex(e => e.Name, "UQ__Ingridie__737584F6600C1C0F").IsUnique();

            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(1);
        });

        modelBuilder.Entity<IngridientWarehousesInformation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("IngridientWarehousesInformations");

            entity.Property(e => e.Ingridient)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(0);
            entity.Property(e => e.Provider)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(4);
            entity.Property(e => e.ReleaseDate)
                .HasColumnOrder(2)
                .HasColumnType("date");
            entity.Property(e => e.StorageLife)
                .HasColumnOrder(3)
                .HasColumnType("date");
            entity.Property(e => e.Weight).HasColumnOrder(1);
        });

        modelBuilder.Entity<IngridientsDish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingridie__3214EC074CCAB7EA");

            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.DishId).HasColumnOrder(1);
            entity.Property(e => e.IngridientId).HasColumnOrder(2);
            entity.Property(e => e.IngridientWeight).HasColumnOrder(3);

            entity.HasOne(d => d.Dish).WithMany(p => p.IngridientsDishes)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IngridientsDishes_To_Dishes");

            entity.HasOne(d => d.Ingridient).WithMany(p => p.IngridientsDishes)
                .HasForeignKey(d => d.IngridientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IngridientsDishes_To_Ingridients");
        });

        modelBuilder.Entity<IngridientsWarehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingridie__3214EC07C9325255");

            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.Cost).HasColumnOrder(5);
            entity.Property(e => e.IngridientId).HasColumnOrder(1);
            entity.Property(e => e.ProviderId).HasColumnOrder(6);
            entity.Property(e => e.ReleaseDate)
                .HasColumnOrder(3)
                .HasColumnType("date");
            entity.Property(e => e.StorageLife)
                .HasColumnOrder(4)
                .HasColumnType("date");
            entity.Property(e => e.Weight)
                .HasColumnOrder(2)
                .HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Ingridient).WithMany(p => p.IngridientsWarehouses)
                .HasForeignKey(d => d.IngridientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IngridientsWarehouses_To_Ingridients");

            entity.HasOne(d => d.Provider).WithMany(p => p.IngridientsWarehouses)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IngridientsWarehouses_To_Providers");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC072FBB5CE4");

            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnOrder(2);
            entity.Property(e => e.CustomerPhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(3);
            entity.Property(e => e.EmployeeId).HasColumnOrder(6);
            entity.Property(e => e.IsCompleted).HasColumnOrder(5);
            entity.Property(e => e.OrderDate)
                .HasColumnOrder(1)
                .HasColumnType("smalldatetime");
            entity.Property(e => e.PaymentMethod).HasColumnOrder(4);

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_To_Employees");
        });

        modelBuilder.Entity<OrderCost>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OrderCosts");

            entity.Property(e => e.CustomerName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnOrder(1);
            entity.Property(e => e.OrderCost1)
                .HasColumnOrder(2)
                .HasColumnName("OrderCost");
            entity.Property(e => e.OrderDate)
                .HasColumnOrder(0)
                .HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<OrderDish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDis__3214EC07366CFAC4");

            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.DishId).HasColumnOrder(2);
            entity.Property(e => e.OrderId).HasColumnOrder(1);

            entity.HasOne(d => d.Dish).WithMany(p => p.OrderDishes)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDishes_To_Dishes");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDishes)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDishes_To_Orders");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Professi__3214EC07A23C8F41");

            entity.HasIndex(e => e.Name, "UQ__Professi__737584F61E3CB58D").IsUnique();

            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(1);
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Provider__3214EC077A40BE52");

            entity.HasIndex(e => e.Name, "UQ__Provider__737584F6CEA0C3C7").IsUnique();

            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnOrder(1);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
