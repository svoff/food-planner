using FoodPlanner.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodPlanner.Data.Tests
{
    [TestClass]
    public class RecipeIOTests
    {
        [TestMethod]
        public void TestReadValidYamlText()
        {
            var recipe = RecipeIO.CreateRecipeFromYaml(ValidSpaghetti);
            Assert.IsNotNull(recipe);
            Assert.AreEqual(recipe.Name, "Spaghetti carbonara (I Morrone)");
            Assert.AreEqual(recipe.DescriptionFile, "spaghetti-carbonara-morrone.md");
            Assert.AreEqual(recipe.Servings, 4);
            Assert.AreEqual(recipe.Ingredients.Length, 6);
            Assert.AreEqual(recipe.Ingredients[0], "400 g spaghetti");
            Assert.AreEqual(recipe.Tags.Length, 6);
            Assert.AreEqual(recipe.Tags[5], "fläskkött");
        }

        private const string ValidSpaghetti = @"---
            namn: Spaghetti carbonara (I Morrone)
            beskrivning: spaghetti-carbonara-morrone.md
            portioner: 4
            ingredienser:
              - 400 g spaghetti
              - 4 äggulor
              - 200 g pancetta
              - 60 g parmesanost
              - svartpeppar
              - salt
            nyckelord:
              - enkelt
              - pasta
              - snabbt
              - vardagsmiddag
              - italiensk mat
              - fläskkött
        ";
    }
}