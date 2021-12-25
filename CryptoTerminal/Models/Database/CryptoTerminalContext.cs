using Microsoft.EntityFrameworkCore;
using System;

namespace CryptoTerminal.Models.Database
{
    public class CryptoTerminalContext: DbContext
    {
        private const string _server = "localhost";

        private const string _user = "root";

        private const string _password = "";

        private const string _database = "CryptoTerminal";

        private static readonly Version _sqlVersion = new System.Version(8, 0, 12);

        public CryptoTerminalContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionStr = ConfigureMySqlConnectionString(_server, _user, _password, _database);
            optionsBuilder.UseMySql(connectionStr, new MySqlServerVersion(_sqlVersion));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminRole = new UserRole { Name = "Admin", Id = 1 };
            var userRole = new UserRole { Name = "User", Id = 2 };

            var admin = new User() { Id = 1, UserName = "admin", Role = adminRole, Password = "qwerty" };

            modelBuilder.Entity<UserRole>().HasData(new[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new[] { admin });

            base.OnModelCreating(modelBuilder);
        }

        private static string ConfigureMySqlConnectionString(string server, string user, string password, string database)
        {
            return $"server={server};user={user};password={password};database={database};";
        }
    }
}
