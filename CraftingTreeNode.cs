using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FactorioRateCalculator
{
    internal class CraftingTreeNode(Recipe baseRecipe, List<CraftingTreeNode> subRecipe)
    {
        public Recipe RootRecipe = baseRecipe;
        public List<CraftingTreeNode> NodeRecipes = subRecipe;

        public HashSet<string> GetUniqueProducts()
        {
            HashSet<string> items = RootRecipe.Ingredients.Select(x => x.Name).ToHashSet();
            foreach (var sub in NodeRecipes)
            {
                items = [..items, ..sub.GetUniqueProducts()];
            }
            return items;
        }
        public static List<Result> FlatteringProduct(List<Result> products)
        {
            List<Result> list = [];
            foreach (Result item in products)
            {
                int index = list.FindIndex((Result result) => result.Product.Name == item.Product.Name);
                if (index != -1)
                {
                    Result tmp = list[index];
                    tmp.Ratio += item.Ratio;
                    list[index] = tmp;
                }
                else
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static CraftingTreeNode GenerateCraftingTree(IReadOnlyList<Recipe> recipes, Recipe recipe)
        {
            List<CraftingTreeNode> rec = [];
            foreach (Product ingredient in recipe.Ingredients)
            {
                Recipe recipe1 = recipes.FirstOrDefault(x => x.Name == ingredient.Name);
                if (!Equals(recipe1, default(Recipe)))
                    rec.Add(GenerateCraftingTree(recipes, recipe1));
            }
            return new CraftingTreeNode(recipe, rec);
        }

        public struct Result(Product product, Recipe recipe, Rational<int> ratio)
        {
            public Product Product = product;
            public Recipe Recipe = recipe;
            public Rational<int> Ratio = ratio;
        }
        public List<Result> RequestedProducts(List<string> hasItems, Rational<int> count)
        {
            IEnumerable<Product> availableIngredients = RootRecipe
                .Ingredients
                .Where(x => hasItems.Contains(x.Name))
            ;
            IEnumerable<CraftingTreeNode?> missingIngredientNodes = RootRecipe
                .Ingredients
                .Where(x => !hasItems.Contains(x.Name))
                .Select(x => NodeRecipes.FirstOrDefault(y => y.RootRecipe.Name == x.Name))
            ;

            return [
                ..availableIngredients.Select(x => new Result(x, RootRecipe, count * x.Amount)),
                ..missingIngredientNodes.SelectMany(
                    x => x?.RequestedProducts(
                        hasItems,
                        new Rational<int>(
                            count * RootRecipe.Ingredients.First(y => x.RootRecipe.Name == y.Name).Amount,
                            NodeRecipes.First(y => y.RootRecipe.Name == x.RootRecipe.Name)?.RootRecipe.Results
                                .First(z => z.Name == x.RootRecipe.Name).Amount ?? 1
                        )
                    ) ?? []
                )
            ];
        }
    }
}
