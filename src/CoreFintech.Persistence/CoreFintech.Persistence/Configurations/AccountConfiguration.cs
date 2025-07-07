using CoreFintech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFintech.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CustomerId).IsRequired();

            // Currency Value Object (Owned Type)
            builder.OwnsOne(x => x.Currency, cb =>
            {
                cb.Property(c => c.Code)
                  .HasColumnName("CurrencyCode")
                  .HasMaxLength(3)
                  .IsRequired();
            });
        }
    }

}
