namespace CoreFintech.Domain.ValueObjects
{
    public sealed class Currency : IEquatable<Currency>
    {
        public string Code { get; }

        private Currency(string code)
        {
            Code = code.ToUpperInvariant();
        }

        public static Currency FromCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Currency code is required");

            if (code.Length != 3) throw new ArgumentException("Currency code must be 3 characters");

            return new Currency(code);
        }

        public override bool Equals(object? obj) => Equals(obj as Currency);
        public bool Equals(Currency? other) => other is not null && Code == other.Code;
        public override int GetHashCode() => Code.GetHashCode();
        public override string ToString() => Code;

        public static readonly Currency TRY = new("TRY");
        public static readonly Currency USD = new("USD");
        public static readonly Currency EUR = new("EUR");
    }
}
