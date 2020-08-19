using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OnlineSellingInventorySystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace OnlineSellingInventorySystem
{
    public class InventoryDbContext : DbContext
    {
        private readonly IConfigurationRoot configuration;

        public InventoryDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public InventoryDbContext(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        public InventoryDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration.GetConnectionString("sql");

            if (string.IsNullOrEmpty(connectionString))
                connectionString = "server=CE201930\\SQLSRVR17;database=oisystem;user id=sa;password=*WebWorks!;";

            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Item> Items { get; set; }
    }
}
