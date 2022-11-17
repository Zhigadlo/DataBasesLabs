using lab5.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lab5.Data
{
    public partial class CafeContext : IdentityDbContext<IdentityUser>
    {
        public CafeContext()
        {
            Database.EnsureCreated();
        }

        public CafeContext(DbContextOptions<CafeContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Ingridient> Ingridients { get; set; } = null!;
        public virtual DbSet<IngridientsDish> IngridientsDishes { get; set; } = null!;
        public virtual DbSet<IngridientsWarehouse> IngridientsWarehouses { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDish> OrderDishes { get; set; } = null!;
        public virtual DbSet<Profession> Professions { get; set; } = null!;
        public virtual DbSet<Provider> Providers { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Dishes__737584F6513A3CE2")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Age).HasDefaultValueSql("((18))");

                entity.Property(e => e.Education)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK_Employees_To_Professions");
            });

            modelBuilder.Entity<Ingridient>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Ingridie__737584F6A66E861B")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IngridientsDish>(entity =>
            {
                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.IngridientsDishes)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_IngridientsDishes_To_Dishes");

                entity.HasOne(d => d.Ingridient)
                    .WithMany(p => p.IngridientsDishes)
                    .HasForeignKey(d => d.IngridientId)
                    .HasConstraintName("FK_IngridientsDishes_To_Ingridients");
            });

            modelBuilder.Entity<IngridientsWarehouse>(entity =>
            {
                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.StorageLife).HasColumnType("date");

                entity.Property(e => e.Weight).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Ingridient)
                    .WithMany(p => p.IngridientsWarehouses)
                    .HasForeignKey(d => d.IngridientId)
                    .HasConstraintName("FK_IngridientsWarehouses_To_Ingridients");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.IngridientsWarehouses)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("FK_IngridientsWarehouses_To_Providers");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CustomerName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate).HasColumnType("smalldatetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_To_Employees");
            });

            modelBuilder.Entity<OrderDish>(entity =>
            {
                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.OrderDishes)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_OrderDishes_To_Dishes");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDishes)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDishes_To_Orders");
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Professi__737584F6D7EB01B2")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Provider__737584F62645BCF3")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
