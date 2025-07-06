using CoreFintech.Domain.ValueObjects;

namespace CoreFintech.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; private set; }
        public string IBAN { get; private set; } = string.Empty;
        public Currency Currency { get; private set; } = Currency.TRY;
        public decimal Balance { get; private set; } = 0;

        private Account() { }
        internal Account(Guid id, string iban, decimal balance, Currency currency, Guid customerId)
        {
            Id = id;
            Balance = balance;
            CustomerId = customerId;
            Currency = currency;
            IBAN = iban;
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
