namespace FactorioRateCalculator
{
    public partial class Form1 : Form
    {
        IReadOnlyList<Recipe> recipes { get; }

        public Form1(IReadOnlyList<Recipe> recipes)
        {
            InitializeComponent();
            this.recipes = recipes;
        }
    }
}
