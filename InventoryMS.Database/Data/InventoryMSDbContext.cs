using InventoryMS.Models.Entities.ApplicationUserModel;
using InventoryMS.Models.Entities.CustomerModel;
using InventoryMS.Models.Entities.LotModel;
using InventoryMS.Models.Entities.PhoneNumberModel;
using InventoryMS.Models.Entities.ProductModels;
using InventoryMS.Models.Entities.SupplierModel;
using InventoryMS.Models.Entities.WarehouseModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Database.Data
{
    public class InventoryMSDbContext(DbContextOptions<InventoryMSDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        //db table
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();

        //product related tables
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Color> Colors => Set<Color>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
        public DbSet<Size> Sizes => Set<Size>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<Lot> Lots => Set<Lot>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<PhoneNumber> PhoneNumbers => Set<PhoneNumber>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }


}
