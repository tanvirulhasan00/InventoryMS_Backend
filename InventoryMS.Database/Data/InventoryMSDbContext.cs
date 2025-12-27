using InventoryMS.Models.Entities.ApplicationUserModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Database.Data
{
    public class InventoryMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public InventoryMSDbContext(DbContextOptions<InventoryMSDbContext> options) : base(options)
        {
        }

        //db table
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }


}
