using CoreFintech.Tenant.Abstractions;

namespace CoreFintech.Tenant
{
    public class TenantContext : ITenantContext
    {
        public Guid TenantId { get; set; } = Guid.Empty;

        public string TenantCode { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;
    }
}
