using System.Text.Json;

namespace FactorioRateCalculator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(ReadRecipesFile()));
        }

        static IReadOnlyList<Recipe> ReadRecipesFile()
        {
            using FileStream fs = new FileStream("../../../recipe.json", FileMode.Open, FileAccess.Read);
            return JsonSerializer.Deserialize<List<Recipe>>(fs, new JsonSerializerOptions {
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