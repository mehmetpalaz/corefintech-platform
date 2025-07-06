namespace CoreFintech.Domain.Abstractions
{
    public interface IHasTenant
    {
        public Guid TenantId { get; set; }
    }
}
