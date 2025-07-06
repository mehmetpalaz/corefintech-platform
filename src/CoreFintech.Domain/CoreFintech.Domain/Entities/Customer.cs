namespace CoreFintech.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        private readonly List<Account> _accounts = new();
        public IReadOnlyCollection<Account> Accounts => _accounts;

        private Customer() { }

        public Customer(Guid id, string fullName, string email)
        {
            Id = id;
            FullName = fullName;
            Email = email;
        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }
    }
}
