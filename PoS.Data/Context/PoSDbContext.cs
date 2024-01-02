﻿using Microsoft.EntityFrameworkCore;


namespace PoS.Data.Context
{
    public class PoSDbContext : DbContext
    {

        public PoSDbContext(DbContextOptions<PoSDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Staff> StaffMembers { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }


        public DbSet<Item> Items { get; set; }





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


