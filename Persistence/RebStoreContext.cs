using Domain.entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class RebStoreContext: DbContext
    {
        public RebStoreContext(DbContextOptions options):base(options){
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ClientNumber>().HasKey(ci => new { ci.client_id, ci.number_id });

        }

        public DbSet<Client> Client{ get; set; }
        public DbSet<ClientNumber> ClientNumber{ get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<List_items> List_Items{ get; set; }
        public DbSet<Number> Number{ get; set; }
        public DbSet<Service> Service{ get; set; }
        public DbSet<Tipo> Tipo{ get; set; }
        public DbSet<User> User { get; set; }

    }
}