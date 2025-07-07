using CoreFintech.Domain.Abstractions;
using CoreFintech.Domain.Entities;
using CoreFintech.Tenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoreFintech.Persistence
{
    public class AppDbContext: DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        public AppDbContext(DbContextOptions<AppDbContext> options, IServiceProvider serviceProvider)
            : base(options)
        {
            _serviceProvider = serviceProvider;
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Account> Accounts => Set<Account>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var tenantContext = _serviceProvider.GetService<ITenantContext>();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IHasTenant).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(AppDbContext)
                        .GetMethod(nameof(SetTenantFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                        .MakeGenericMethod(entityType.ClrType);

                    method.Invoke(null, new object[] { modelBuilder, tenantContext! });
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        private static void SetTenantFilter<TEntity>(ModelBuilder modelBuilder, ITenantContext tenantContext)
            where TEntity : class, IHasTenant
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => e.TenantId == tenantContext.TenantId);
        }
    }
}
