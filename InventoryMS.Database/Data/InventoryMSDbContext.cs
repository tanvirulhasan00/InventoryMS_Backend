using InventoryMS.Models.Entities.ApplicationUserModel;
using InventoryMS.Models.Entities.CustomerModel;
using InventoryMS.Models.Entities.ProductModels;
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
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Brand> Brands => Set<Brand>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }


}
