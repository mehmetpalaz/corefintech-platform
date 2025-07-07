namespace CoreFintech.Domain.ValueObjects
{
    public sealed record Currency
    {
        public string Code { get; init; }

        // EF Core için parametresiz ctor
        private Currency() { }

        // Factory method
        private Currency(string code) => Code = code.ToUpperInvariant();

        public static Currency FromCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Currency code is required");
            if (code.Length != 3) throw new ArgumentException("Must be 3 chars");
            return new Currency(code);
        }
    }

}
