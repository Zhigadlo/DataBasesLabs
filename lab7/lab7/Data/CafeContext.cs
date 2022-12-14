using Microsoft.EntityFrameworkCore;

namespace lab7.Data
{
    partial class CafeContext : DbContext
    {
        public CafeContext()
        {
        }

        public CafeContext(DbContextOptions<CafeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Ingridient> Ingridients { get; set; }

        public virtual DbSet<IngridientsDish> IngridientsDishes { get; set; }

        public virtual DbSet<IngridientsWarehouse> IngridientsWarehouses { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDish> OrderDishes { get; set; }

        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Dishes__737584F6513A3CE2").IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.ProfessionId, "IX_Employees_ProfessionId");

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

                entity.HasOne(d => d.Profession).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK_Employees_To_Professions");
            });

            modelBuilder.Entity<Ingridient>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Ingridie__737584F6A66E861B").IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IngridientsDish>(entity =>
            {
                entity.HasIndex(e => e.DishId, "IX_IngridientsDishes_DishId");

                entity.HasIndex(e => e.IngridientId, "IX_IngridientsDishes_IngridientId");

                entity.HasOne(d => d.Dish).WithMany(p => p.IngridientsDishes)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_IngridientsDishes_To_Dishes");

                entity.HasOne(d => d.Ingridient).WithMany(p => p.IngridientsDishes)
                    .HasForeignKey(d => d.IngridientId)
                    .HasConstraintName("FK_IngridientsDishes_To_Ingridients");
            });

            modelBuilder.Entity<IngridientsWarehouse>(entity =>
            {
                entity.HasIndex(e => e.IngridientId, "IX_IngridientsWarehouses_IngridientId");

                entity.HasIndex(e => e.ProviderId, "IX_IngridientsWarehouses_ProviderId");

                entity.Property(e => e.ReleaseDate).HasColumnType("date");
                entity.Property(e => e.StorageLife).HasColumnType("date");
                entity.Property(e => e.Weight).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Ingridient).WithMany(p => p.IngridientsWarehouses)
                    .HasForeignKey(d => d.IngridientId)
                    .HasConstraintName("FK_IngridientsWarehouses_To_Ingridients");

                entity.HasOne(d => d.Provider).WithMany(p => p.IngridientsWarehouses)
                    .HasForeignKey(d => d.ProviderId)
                    .HasConstraintName("FK_IngridientsWarehouses_To_Providers");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId, "IX_Orders_EmployeeId");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.CustomerPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.OrderDate).HasColumnType("smalldatetime");

                entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_To_Employees");
            });

            modelBuilder.Entity<OrderDish>(entity =>
            {
                entity.HasIndex(e => e.DishId, "IX_OrderDishes_DishId");

                entity.HasIndex(e => e.OrderId, "IX_OrderDishes_OrderId");

                entity.HasOne(d => d.Dish).WithMany(p => p.OrderDishes)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_OrderDishes_To_Dishes");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderDishes)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDishes_To_Orders");
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Professi__737584F6D7EB01B2")
                    .IsUnique()
                    .HasFilter("([Name] IS NOT NULL)");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Provider__737584F62645BCF3").IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
