namespace CoreFintech.Tenant.Abstractions
{
    public interface ITenantContext
    {
        Guid TenantId { get;}
        string TenantCode { get; }
        string CountryCode { get; }
    }
}
