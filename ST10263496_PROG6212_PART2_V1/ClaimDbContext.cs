using Microsoft.EntityFrameworkCore;
using ST10263496_PROG6212_PART2_V1.Models;

namespace ST10263496_PROG6212_PART2_V1.Data
{
    // This class represents the database context for the Claim application.
    // It inherits from DbContext to provide database access and management functionality.
    public class ClaimDbContext : DbContext
    {
        // DbSet property to represent the Claims table in the database.
        // This will allow CRUD operations on Claim entities.
        public DbSet<Claim> Claims { get; set; }

        // This method configures the database connection settings.
        // In this case, it uses an SQLite database and specifies the database file 'claims.db'.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // UseSqlite specifies that the database provider is SQLite.
            // The connection string 'Data Source=claims.db' means that the database file will be created
            // and stored locally as 'claims.db'.
            optionsBuilder.UseSqlite("Data Source=claims.db");
        }
    }
}
