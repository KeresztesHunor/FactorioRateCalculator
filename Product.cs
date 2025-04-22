namespace FactorioRateCalculator
{
    public enum ProductType : byte
    {
        Item,
        Fluid
    }

    public struct Product
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public ProductType Type { get; set; }

        public override string ToString() => $"{{ {nameof(Type).ToLower()}: \"{Type.ToString().PascalToKebabCase()}\", {nameof(Name).ToLower()}: \"{Name}\", {nameof(Amount).ToLower()}: {Amount} }}";
    }
}
