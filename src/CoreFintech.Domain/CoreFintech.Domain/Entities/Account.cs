using CoreFintech.Domain.Abstractions;
using CoreFintech.Domain.ValueObjects;

namespace CoreFintech.Domain.Entities
{
    public class Account : IHasTenant
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; private set; }
        public string IBAN { get; private set; } = string.Empty;
        public Currency Currency { get; private set; }
        public decimal Balance { get; private set; } = 0;
        public Guid TenantId { get; set; }

        private Account() { }
        internal Account(Guid id, string iban, decimal balance, Currency currency, Guid customerId, Guid tenantId)
        {
            Id = id;
            Balance = balance;
            CustomerId = customerId;
            Currency = currency;
            IBAN = iban;
            TenantId = tenantId;
        }

        public void Credit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");

            Balance += amount;
        }

        public void Debit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");

            if (amount > Balance) throw new InvalidOperationException("Insufficient balance");

            Balance -= amount;
        }

    }
}
