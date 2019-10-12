using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SympleAppointments.Domain;

namespace SympleAppointments.Persistence
{
    public class DataContext : ApiAuthorizationDbContext<AppUser>
    {
        public DataContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Annotation> Annotations { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Appointment>(a =>
            {
                a.HasOne(c => c.Client);
                a.HasMany(n => n.Annotations).WithOne(n => n.Appointment);
            });

           
        }
    }
}

