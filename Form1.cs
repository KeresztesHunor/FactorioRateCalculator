using System.Text.Json;

namespace FactorioRateCalculator
{
    public partial class Form1 : Form
    {
        IReadOnlyList<Recipe> recipes { get; }

        public Form1()
        {
            InitializeComponent();
            recipes = ReadRecipesFile();
        }

        static IReadOnlyList<Recipe> ReadRecipesFile()
        {
            using FileStream fs = new FileStream("../../../recipe.json", FileMode.Open, FileAccess.Read);
            return JsonSerializer.Deserialize<List<Recipe>>(fs, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                PropertyNameCaseInsensitive = true,
                Converters = {
                    new EnumTypeJsonConverter<ProductType>(Utils.KebabToPascalCase, Utils.PascalToKebabCase),
                    new EnumTypeJsonConverter<ProductionCategory>(Utils.KebabToPascalCase, Utils.PascalToKebabCase)
                }
            })!;
        }
    }
}
