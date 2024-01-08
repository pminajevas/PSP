using Microsoft.EntityFrameworkCore;
using PoS.Core.Entities;

namespace PoS.Infrastructure.Context
{
    public interface IPoSDBContext
    {
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
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }
        public DbSet<Item> Items { get; set; }
        DbContext Instance { get; }
    }
}
