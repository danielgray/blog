using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Troubadour.Models
{
    public class TroubadourContext : IdentityDbContext<TroubadourUser>
    {
        public TroubadourContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Story> Stories { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Startup.Configuration["Data:DefaultConnection:ConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

    }
}
