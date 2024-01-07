using Microsoft.EntityFrameworkCore;
using PoS.Core.Entities;

namespace PoS.Infrastructure.Context
{
    public class PoSDBContext : DbContext
    {
        public PoSDBContext(DbContextOptions<PoSDBContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<Coupon> Coupons { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<Tax> Taxes { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Business> Businesses { get; set; } = default!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
        public DbSet<Service> Services { get; set; } = default!;
        public DbSet<Staff> StaffMembers { get; set; } = default!;
        public DbSet<Appointment> Appointments { get; set; } = default!;
        public DbSet<Discount> Discounts { get; set; } = default!;
        public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Coupon>()
                .Property(e => e.Validity)
                .HasConversion<int>();

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.Type)
                .HasConversion<int>();

            modelBuilder.Entity<Payment>()
                .Property(e => e.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Tax>()
                .Property(e => e.Category)
                .HasConversion<int>();
            modelBuilder.Entity<Customer>()
                .HasKey(c => new { c.Id, c.BusinessId });

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.NewGuid(), RoleName = "Admin", Description = "Administrator has access to all operations" },
                new Role { Id = Guid.NewGuid(), RoleName = "Manager", Description = "Manager has access to all operations in the business"},
                new Role { Id = Guid.NewGuid(), RoleName = "Staff", Description = "Staff has access to most common operations in the business" },
                new Role { Id = Guid.NewGuid(), RoleName = "Customer", Description = "Customer has access to operations related to customer self-service" }
            );
        }
    }
}



