using CoreFintech.Tenant.Abstractions;

namespace CoreFintech.Tenant
{
    public class PlaygroundTenantContext : ITenantContext
    {
        public Guid TenantId => Guid.Parse("11111111-1111-1111-1111-111111111111"); // örnek tenant

        public string TenantCode => string.Empty;

        public string CountryCode => string.Empty;
    }
}
