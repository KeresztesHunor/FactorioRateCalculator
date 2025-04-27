using System.Text.Json;
using System.Windows.Forms;

namespace FactorioRateCalculator
{
    public partial class Form1 : Form
    {
        IReadOnlyList<Recipe> recipes { get; }

        LastCalculatedRatio? lastCalculatedRatio;

        public Form1()
        {
            InitializeComponent();
            recipes = ReadRecipesFile();
            lastCalculatedRatio = null;
            recipesListBox.DataSource = recipes;
        }

        static IReadOnlyList<Recipe> ReadRecipesFile()
        {
            using FileStream fs = new FileStream("recipe.json", FileMode.Open, FileAccess.Read);
            return JsonSerializer.Deserialize<List<Recipe>>(fs, new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                PropertyNameCaseInsensitive = true,
                Converters = {
                    new EnumTypeJsonConverter<ProductType>(Utils.KebabToPascalCase, Utils.PascalToKebabCase),
                    new EnumTypeJsonConverter<ProductionCategory>(Utils.KebabToPascalCase, Utils.PascalToKebabCase)
                }
            })!;
        }

        private void LementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastCalculatedRatio is not null)
            {
                LastCalculatedRatio lastCalculatedRatio = this.lastCalculatedRatio.Value;
                using FileStream fs = new FileStream($"../../../Results/{lastCalculatedRatio.RecipeName}.txt", FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("ingredient ratio data:");
                sw.WriteLine(string.Join('\n', lastCalculatedRatio.IngredientRatioData));
                sw.WriteLine("main bus has:");
                sw.WriteLine(string.Join('\n', lastCalculatedRatio.MainBusItems));
            }
        }

        private void RecipesListBox_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = $"{((Recipe)e.ListItem!).Name} ({string.Join(", ", ((Recipe)e.ListItem).Ingredients.Select(x => $"{x.Name} {x.Amount}x"))})";
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            Recipe selectedRecipe = (Recipe)recipesListBox.SelectedItem!;
            CraftingTreeNode rec = CraftingTreeNode.GenerateCraftingTree(recipes, selectedRecipe);
            List<CraftingTreeNode.Result> result = CraftingTreeNode.FlatteringProduct(rec
                .RequestedProducts(
                    checkedListBox1.CheckedItems
                        .OfType<string>()
                        .ToList(),
                    (int)numericUpDown1.Value
                )
            );
            IEnumerable<string> ingredientRatioData = result.Select(x => $"{x.Product.Name} {x.Ratio.ValueD}x ({(x.Ratio / result.Select(x => x.Ratio).Sum()).ValueD:P2})");
            resultListBox.DataSource = ingredientRatioData.ToArray();
            lastCalculatedRatio = new LastCalculatedRatio(selectedRecipe.Name, ingredientRatioData, checkedListBox1.CheckedItems.OfType<string>());
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            recipesListBox.DataSource = recipes
                .Where(x => x.Name.Contains(textBox1.Text, StringComparison.CurrentCultureIgnoreCase))
                .ToList()
            ;
        }

        private void RecipesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(CraftingTreeNode.GenerateCraftingTree(recipes, (Recipe)recipesListBox.SelectedItem!).GetUniqueProducts().ToArray());
        }

        readonly struct LastCalculatedRatio(string recipeName, IEnumerable<string> ingredientRatioData, IEnumerable<string> mainBusItems)
        {
            public string RecipeName { get; } = recipeName;

            public IEnumerable<string> IngredientRatioData { get; } = ingredientRatioData;

            public IEnumerable<string> MainBusItems { get; } = mainBusItems;
        }
    }
}
