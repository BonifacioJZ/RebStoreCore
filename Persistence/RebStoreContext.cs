using Domain.entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class RebStoreContext: IdentityDbContext<User>
    {
        public RebStoreContext(DbContextOptions options):base(options){
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            builder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            builder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));
            builder.Entity<ClientNumber>().HasKey(ci => new { ci.ClientId, ci.NumberId });

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