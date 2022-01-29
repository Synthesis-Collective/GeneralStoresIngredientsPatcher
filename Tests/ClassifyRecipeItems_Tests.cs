using GeneralStoresIngredientsPatcher;
using Mutagen.Bethesda;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Skyrim;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Order;
using Xunit;

namespace Tests
{
    public class ClassifyRecipeItems_Tests
    {
        public static readonly ModKey masterModKey = ModKey.FromNameAndExtension("master.esp");

        internal static void AssertUnchanged(Program program)
        {
            Assert.Empty(program.allSmithingSet);
            Assert.Empty(program.smeltingSet);
            Assert.Empty(program.smithingSet);
            Assert.Empty(program.tanningSet);
            Assert.Empty(program.hearthFiresConstructionSet);
            Assert.Empty(program.hearthFiresIngredientSet);
            Assert.Empty(program.alchemyAndCookingSet);
            Assert.Empty(program.alchemyAndSmithingSet);
            Assert.Empty(program.allFoodSet);
            Assert.Empty(program.cookedFoodSet);
            Assert.Empty(program.rawFoodSet);
        }

        [Fact]
        public static void RecipeWithNoWorkbench() {
            SkyrimMod masterMod = new(masterModKey, SkyrimRelease.SkyrimSE);

            masterMod.ConstructibleObjects.AddNew("newRecipe");

            var linkCache = masterMod.ToImmutableLinkCache();

            var loadOrder = new LoadOrder<IModListingGetter<ISkyrimModGetter>>
            {
                new ModListing<ISkyrimModGetter>(masterMod, true)
            };

            Program program = new(loadOrder, linkCache, null!, GameRelease.SkyrimSE);

            program.ClassifyRecipeItems(new HashSet<IFormLinkGetter<IItemGetter>>());

            AssertUnchanged(program);
        }

        [Fact]
        public static void RecipeWithUnhandledWorkbench()
        {
            SkyrimMod masterMod = new(masterModKey, SkyrimRelease.SkyrimSE);

            var newRecipe = masterMod.ConstructibleObjects.AddNew("newRecipe");

            newRecipe.WorkbenchKeyword.SetTo(Skyrim.Keyword.WICraftingSmithingTempering);

            var linkCache = masterMod.ToImmutableLinkCache();

            var loadOrder = new LoadOrder<IModListingGetter<ISkyrimModGetter>>
            {
                new ModListing<ISkyrimModGetter>(masterMod, true)
            };

            Program program = new(loadOrder, linkCache, null!, GameRelease.SkyrimSE);

            program.ClassifyRecipeItems(new HashSet<IFormLinkGetter<IItemGetter>>());

            AssertUnchanged(program);
        }

        [Fact]
        public static void CookPotRecipeResult()
        {
            SkyrimMod masterMod = new(masterModKey, SkyrimRelease.SkyrimSE);

            var newRecipe = masterMod.ConstructibleObjects.AddNew("newRecipe");

            newRecipe.WorkbenchKeyword.SetTo(Skyrim.Keyword.CraftingCookpot);

            var result = Skyrim.Ingredient.CharredSkeeverHide;

            newRecipe.CreatedObject.SetTo(result);

            var linkCache = masterMod.ToImmutableLinkCache();

            var loadOrder = new LoadOrder<IModListingGetter<ISkyrimModGetter>>
            {
                new ModListing<ISkyrimModGetter>(masterMod, true)
            };

            Program program = new(loadOrder, linkCache, null!, GameRelease.SkyrimSE);

            program.ClassifyRecipeItems(new HashSet<IFormLinkGetter<IItemGetter>>());

            Assert.Empty(program.allSmithingSet);
            Assert.Empty(program.smeltingSet);
            Assert.Empty(program.smithingSet);
            Assert.Empty(program.tanningSet);
            Assert.Empty(program.hearthFiresConstructionSet);
            Assert.Empty(program.hearthFiresIngredientSet);
            Assert.Empty(program.alchemyAndCookingSet);
            Assert.Empty(program.alchemyAndSmithingSet);
            //Assert.Empty(program.allFoodSet);
            //Assert.Empty(program.cookedFoodSet);
            Assert.Empty(program.rawFoodSet);

            Assert.Single(program.allFoodSet);
            Assert.Single(program.cookedFoodSet);

            Assert.Contains(result, program.allFoodSet);
            Assert.Contains(result, program.cookedFoodSet);
        }

        [Fact]
        public static void GrainMillRecipeResult()
        {
            SkyrimMod masterMod = new(masterModKey, SkyrimRelease.SkyrimSE);

            var newRecipe = masterMod.ConstructibleObjects.AddNew("newRecipe");

            newRecipe.WorkbenchKeyword.SetTo(Skyrim.Keyword.isGrainMill);

            var result = Skyrim.Ingredient.BoneMeal;

            newRecipe.CreatedObject.SetTo(result);

            var linkCache = masterMod.ToImmutableLinkCache();

            var loadOrder = new LoadOrder<IModListingGetter<ISkyrimModGetter>>
            {
                new ModListing<ISkyrimModGetter>(masterMod, true)
            };

            Program program = new(loadOrder, linkCache, null!, GameRelease.SkyrimSE);

            program.ClassifyRecipeItems(new HashSet<IFormLinkGetter<IItemGetter>>());

            Assert.Empty(program.allSmithingSet);
            Assert.Empty(program.smeltingSet);
            Assert.Empty(program.smithingSet);
            Assert.Empty(program.tanningSet);
            Assert.Empty(program.hearthFiresConstructionSet);
            Assert.Empty(program.hearthFiresIngredientSet);
            Assert.Empty(program.alchemyAndCookingSet);
            Assert.Empty(program.alchemyAndSmithingSet);
            Assert.Empty(program.allFoodSet);
            //Assert.Empty(program.cookedFoodSet);
            //Assert.Empty(program.rawFoodSet);

            Assert.Single(program.rawFoodSet);
            Assert.Single(program.cookedFoodSet);

            Assert.Contains(result, program.rawFoodSet);
            Assert.Contains(result, program.cookedFoodSet);
        }
    }
}
