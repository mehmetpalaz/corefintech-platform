using CoreFintech.Tenant.Abstractions;
using Microsoft.AspNetCore.Http;

namespace CoreFintech.Tenant
{
    public class TenantContextMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext)
        {
            if (tenantContext is TenantContext concreteContext)
            {
                if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantId))
                    concreteContext.TenantId = Guid.Parse(tenantId!);

                if (context.Request.Headers.TryGetValue("X-Tenant-Code", out var tenantCode))
                    concreteContext.TenantCode = tenantCode!;

                if (context.Request.Headers.TryGetValue("X-Country", out var country))
                    concreteContext.CountryCode = country!;
            }

            await _next(context);
        }
    }
}
