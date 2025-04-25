using System.Text.Json;
using System.Windows.Forms;

namespace FactorioRateCalculator
{
    public partial class Form1 : Form
    {
        IReadOnlyList<Recipe> recipes { get; }

        public Form1()
        {
            InitializeComponent();
            recipes = ReadRecipesFile();
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

        private void BeolvasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            ReadRecipesFile();
            recipesListBox.DataSource = recipes;
        }

        private void RecipesListBox_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = $"{((Recipe)e.ListItem!).Name} ({string.Join(", ", ((Recipe)e.ListItem).Ingredients.Select(x => $"{x.Name} {x.Amount}x"))})";
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            CraftingTreeNode rec = CraftingTreeNode.GenerateCraftingTree(recipes, (Recipe)recipesListBox.SelectedItem!);
            List<CraftingTreeNode.Result> result = CraftingTreeNode.FlatteringProduct(rec
                .RequestedProducts(
                    checkedListBox1.CheckedItems
                        .OfType<string>()
                        .ToList(),
                    (int)numericUpDown1.Value
                )
            );
            resultListBox.DataSource = result.Select(x => $"{x.Product.Name} {x.Ratio.ValueD}x ({(x.Ratio / result.Select(x => x.Ratio).Sum()).ValueD:P2})").ToList();
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
    }
}
