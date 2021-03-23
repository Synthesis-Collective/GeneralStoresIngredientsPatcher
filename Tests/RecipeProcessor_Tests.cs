using GeneralStoresIngredientsPatcher;
using Mutagen.Bethesda;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Skyrim;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class RecipeProcessor_Tests
    {
        public static readonly ModKey masterModKey = ModKey.FromNameAndExtension("master.esp");

        [Fact]
        public static void RecipeWithNoItems()
        {
            SkyrimMod masterMod = new(masterModKey, SkyrimRelease.SkyrimSE);

            var newRecipe = masterMod.ConstructibleObjects.AddNew("newRecipe");

            var linkCache = masterMod.ToImmutableLinkCache();

            HashSet<IFormLinkGetter<IItemGetter>> specificSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> allSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> ingredientSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> doNotUnburdenFormKeys = new();

            RecipeProcessor recipeProcessor = new(specificSet, allSet, ingredientSet, linkCache, doNotUnburdenFormKeys);


            recipeProcessor.ClassifyRecipeIngredients(newRecipe);


            Assert.Empty(specificSet);
            Assert.Empty(allSet);
            Assert.Empty(ingredientSet);
        }

        [Fact]
        public static void RecipeWithEmptyItems()
        {
            SkyrimMod masterMod = new(masterModKey, SkyrimRelease.SkyrimSE);

            var newRecipe = masterMod.ConstructibleObjects.AddNew("newRecipe");

            newRecipe.Items = new();

            var linkCache = masterMod.ToImmutableLinkCache();

            HashSet<IFormLinkGetter<IItemGetter>> specificSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> allSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> ingredientSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> doNotUnburdenFormKeys = new();

            RecipeProcessor recipeProcessor = new(specificSet, allSet, ingredientSet, linkCache, doNotUnburdenFormKeys);


            recipeProcessor.ClassifyRecipeIngredients(newRecipe);


            Assert.Empty(specificSet);
            Assert.Empty(allSet);
            Assert.Empty(ingredientSet);
        }

        [Fact]
        public static void RecipeWithForbiddenItem()
        {
            var theItemFormLink = Skyrim.MiscItem.Gold001;

            SkyrimMod masterMod = new(theItemFormLink.FormKey.ModKey, SkyrimRelease.SkyrimSE);

            var theItem = new MiscItem(theItemFormLink.FormKey, masterMod.SkyrimRelease);

            masterMod.MiscItems.Add(theItem);

            var newRecipe = masterMod.ConstructibleObjects.AddNew("newRecipe");

            (newRecipe.Items = new()).Add(new()
            {
                Item = new()
                {
                    Item = Skyrim.MiscItem.Gold001
                }
            });

            var linkCache = masterMod.ToImmutableLinkCache();

            HashSet<IFormLinkGetter<IItemGetter>> specificSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> allSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> ingredientSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> doNotUnburdenFormKeys = new();

            doNotUnburdenFormKeys.Add(Skyrim.MiscItem.Gold001);

            RecipeProcessor recipeProcessor = new(specificSet, allSet, ingredientSet, linkCache, doNotUnburdenFormKeys);


            recipeProcessor.ClassifyRecipeIngredients(newRecipe);


            Assert.Empty(specificSet);
            Assert.Empty(allSet);
            Assert.Empty(ingredientSet);
        }

        [Fact]
        public static void IngestibleItem()
        {
            var theItemFormLink = Skyrim.Ingestible.Ale;

            SkyrimMod masterMod = new(theItemFormLink.FormKey.ModKey, SkyrimRelease.SkyrimSE);

            var theItem = new Ingestible(theItemFormLink.FormKey, masterMod.SkyrimRelease);

            masterMod.Ingestibles.Add(theItem);

            var linkCache = masterMod.ToImmutableLinkCache();

            HashSet<IFormLinkGetter<IItemGetter>> specificSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> allSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> ingredientSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> doNotUnburdenFormKeys = new();

            RecipeProcessor recipeProcessor = new(specificSet, allSet, ingredientSet, linkCache, doNotUnburdenFormKeys);


            recipeProcessor.ClassifyIngredient(theItemFormLink);


            Assert.Empty(specificSet);
            Assert.Empty(allSet);
            Assert.Single(ingredientSet);
        }

        [Fact]
        public static void IngredientItem()
        {
            var theItemFormLink = Skyrim.Ingredient.SkeeverTail;

            SkyrimMod masterMod = new(theItemFormLink.FormKey.ModKey, SkyrimRelease.SkyrimSE);

            var theItem = new Ingredient(theItemFormLink.FormKey, masterMod.SkyrimRelease);

            masterMod.Ingredients.Add(theItem);

            var linkCache = masterMod.ToImmutableLinkCache();

            HashSet<IFormLinkGetter<IItemGetter>> specificSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> allSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> ingredientSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> doNotUnburdenFormKeys = new();

            RecipeProcessor recipeProcessor = new(specificSet, allSet, ingredientSet, linkCache, doNotUnburdenFormKeys);


            recipeProcessor.ClassifyIngredient(theItemFormLink);


            Assert.Empty(specificSet);
            Assert.Empty(allSet);
            Assert.Single(ingredientSet);
        }
        [Fact]
        public static void ArmorIngredient()
        {
            var theItemFormLink = Skyrim.Armor.ArmorIronShield;

            SkyrimMod masterMod = new(theItemFormLink.FormKey.ModKey, SkyrimRelease.SkyrimSE);

            var theItem = new Armor(theItemFormLink.FormKey, masterMod.SkyrimRelease);

            masterMod.Armors.Add(theItem);

            var linkCache = masterMod.ToImmutableLinkCache();

            HashSet<IFormLinkGetter<IItemGetter>> specificSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> allSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> ingredientSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> doNotUnburdenFormKeys = new();

            RecipeProcessor recipeProcessor = new(specificSet, allSet, ingredientSet, linkCache, doNotUnburdenFormKeys);


            recipeProcessor.ClassifyIngredient(theItemFormLink);


            Assert.Empty(specificSet);
            Assert.Empty(allSet);
            Assert.Empty(ingredientSet);
        }

        [Fact]
        public static void ArmorIntermediateIngredient()
        {
            SkyrimMod masterMod = new("master.esp", SkyrimRelease.SkyrimSE);

            var theItem = new Armor(masterMod, "Gold Ring Shank");

            var theItemFormLink = theItem.AsLink();

            masterMod.Armors.Add(theItem);

            var linkCache = masterMod.ToImmutableLinkCache();

            HashSet<IFormLinkGetter<IItemGetter>> specificSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> allSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> ingredientSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> doNotUnburdenFormKeys = new();

            RecipeProcessor recipeProcessor = new(specificSet, allSet, ingredientSet, linkCache, doNotUnburdenFormKeys);


            recipeProcessor.ClassifyIngredient(theItemFormLink);


            Assert.Single(specificSet);
            Assert.Single(allSet);
            Assert.Empty(ingredientSet);
        }

        [Fact]
        public static void WeaponIngredient()
        {
            var theItemFormLink = Skyrim.Weapon.IronDagger;

            SkyrimMod masterMod = new(theItemFormLink.FormKey.ModKey, SkyrimRelease.SkyrimSE);

            var theItem = new Weapon(theItemFormLink.FormKey, masterMod.SkyrimRelease);

            masterMod.Weapons.Add(theItem);

            var linkCache = masterMod.ToImmutableLinkCache();

            HashSet<IFormLinkGetter<IItemGetter>> specificSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> allSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> ingredientSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> doNotUnburdenFormKeys = new();

            RecipeProcessor recipeProcessor = new(specificSet, allSet, ingredientSet, linkCache, doNotUnburdenFormKeys);


            recipeProcessor.ClassifyIngredient(theItemFormLink);


            Assert.Empty(specificSet);
            Assert.Empty(allSet);
            Assert.Empty(ingredientSet);
        }

        [Fact]
        public static void MiscIngredient()
        {
            var theItemFormLink = Skyrim.MiscItem.IngotIron;

            SkyrimMod masterMod = new(theItemFormLink.FormKey.ModKey, SkyrimRelease.SkyrimSE);

            var theItem = new MiscItem(theItemFormLink.FormKey, masterMod.SkyrimRelease);

            masterMod.MiscItems.Add(theItem);

            var linkCache = masterMod.ToImmutableLinkCache();

            HashSet<IFormLinkGetter<IItemGetter>> specificSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> allSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> ingredientSet = new();
            HashSet<IFormLinkGetter<IItemGetter>> doNotUnburdenFormKeys = new();

            RecipeProcessor recipeProcessor = new(specificSet, allSet, ingredientSet, linkCache, doNotUnburdenFormKeys);


            recipeProcessor.ClassifyIngredient(theItemFormLink);


            Assert.Single(specificSet);
            Assert.Single(allSet);
            Assert.Empty(ingredientSet);
        }
    }
}
