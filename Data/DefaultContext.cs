using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Product.Model;
using System.Collections.Generic;

namespace Product.Data
{
    public class DefaultContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                OnConfiguring(optionsBuilder.UseSqlServer(GetSqlServerConnection()));
            }
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Products> Products { get; set; }
        private static string GetSqlServerConnection()
        {
            SqlConnectionStringBuilder connectionBuilder = new()
            {
                ConnectTimeout = 0,
                DataSource = "DESKTOP-KO7K9QI",
                InitialCatalog = "StudentManagementSystem",
                TrustServerCertificate = true,
                MultipleActiveResultSets = true,
                IntegratedSecurity = true
            };
            return connectionBuilder.ConnectionString;
        }
    }
}
