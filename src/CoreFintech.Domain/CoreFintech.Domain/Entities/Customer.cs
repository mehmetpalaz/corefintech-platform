﻿using CoreFintech.Domain.Abstractions;
using CoreFintech.Domain.ValueObjects;

namespace CoreFintech.Domain.Entities
{
    public class Customer : IHasTenant
    {
        public Guid Id { get; set; }
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        private readonly List<Account> _accounts = new();
        public IReadOnlyCollection<Account> Accounts => _accounts;

        public Guid TenantId { get; set; }

        private Customer() { }

        public Customer(Guid id, string fullName, string email, Guid tenantId)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            TenantId = tenantId;
        }

        public Account CreateAccount(string iban, Currency currency, decimal balance)
        {
            var account = new Account(Guid.NewGuid(), iban, balance, currency, this.Id, this.TenantId);
            _accounts.Add(account);
            return account;
        }
    }
}
