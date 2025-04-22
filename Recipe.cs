namespace FactorioRateCalculator
{
    public enum ProductionCategory : byte
    {
        Crafting,
        AdvancedCrafting,
        Smelting,
        OilProcessing,
        CraftingWithFluid,
        Chemistry,
        RocketBuilding,
        Centrifuging
    }

    public struct Recipe()
    {
        public string Name { get; set; }
        public float EnergyRequired { get; set; } = 0.5f;
        public ProductionCategory? Category { get; set; }
        public List<Product> Ingredients { get; set; }
        public List<Product> Results { get; set; }

        public override string ToString() => $"{{\n\t{nameof(Name).ToLower()}: \"{Name}\",\n\t{nameof(Category).ToLower()}: \"{Category?.ToString().PascalToKebabCase() ?? "null"}\",\n{ProductListToString(Ingredients, nameof(Ingredients))},\n{ProductListToString(Results, nameof(Results))}\n}}";

        static string ProductListToString(IReadOnlyList<Product> list, string listName)
        {
            string s = $"\t{listName.ToLower()}: [\n";
            for (int i = 0; i < list.Count - 1; i++)
            {
                s += $"\t\t{list[i]},\n";
            }
            return $"{s}\t\t{list[^1]}\n\t]";
        }
    }
}
