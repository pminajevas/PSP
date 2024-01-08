using Microsoft.EntityFrameworkCore;
using PoS.Core.Entities;

namespace PoS.Infrastructure.Context
{
    public class PoSDBContext : DbContext, IPoSDBContext
    {
        public PoSDBContext(DbContextOptions<PoSDBContext> options) : base(options) { }

        public DbContext Instance => this;
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

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.Parse("E039F4E6-D3BF-4318-82FD-60E95D88F40F"), RoleName = "Admin", Description = "Administrator has access to all operations" },
                new Role { Id = Guid.Parse("745C6724-F255-4C2E-9976-BE210824B534"), RoleName = "Manager", Description = "Manager has access to all operations in the business"},
                new Role { Id = Guid.Parse("92E68FF5-37FE-4598-91B0-448F4C3D44C3"), RoleName = "Staff", Description = "Staff has access to most common operations in the business" },
                new Role { Id = Guid.Parse("37E76957-C0E9-41E1-B7FC-26ABE102A6E0"), RoleName = "Customer", Description = "Customer has access to operations related to customer self-service" }
            );
        }
    }
}



