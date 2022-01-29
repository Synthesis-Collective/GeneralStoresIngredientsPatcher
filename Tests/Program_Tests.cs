using GeneralStoresIngredientsPatcher;
using Mutagen.Bethesda;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using System.Linq;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Plugins.Order;
using Xunit;

namespace Tests
{
    public class Program_Tests
    {
        [Fact]
        public static void DoNothing()
        {
            ModKey modKey = ModKey.FromNameAndExtension("Patch.esp");

            ISkyrimMod patchMod = new SkyrimMod(modKey,SkyrimRelease.SkyrimSE);

            LoadOrder<IModListingGetter<ISkyrimModGetter>> loadOrder = new()
            {
                new ModListing<ISkyrimMod>(patchMod, true)
            };

            ILinkCache<ISkyrimMod, ISkyrimModGetter> linkCache = loadOrder.ToImmutableLinkCache();

            var program = new Program(loadOrder, linkCache, patchMod, GameRelease.SkyrimSE, new());

            program.RunPatch();
        }

        [Fact]
        public static void ClassifyAThing()
        {
            var theItemFormLink = Skyrim.Ingestible.Ale;

            SkyrimMod masterMod = new(theItemFormLink.FormKey.ModKey, SkyrimRelease.SkyrimSE);

            var theItem = new Ingestible(theItemFormLink.FormKey, masterMod.SkyrimRelease);

            masterMod.Ingestibles.Add(theItem);

            ModKey modKey = ModKey.FromNameAndExtension("Patch.esp");

            ISkyrimMod patchMod = new SkyrimMod(modKey, SkyrimRelease.SkyrimSE);

            var formListLink = GeneralStores.FormList.xGSxAlchCookFLST;
            var flst = new FormList(formListLink.FormKey, patchMod.SkyrimRelease)
            {
                EditorID = nameof(GeneralStores.FormList.xGSxAlchCookFLST)
            };
            patchMod.FormLists.Add(flst);

            var newRecipe = patchMod.ConstructibleObjects.AddNew("newRecipe");
            (newRecipe.Items ??= new()) .Add(new()
            {
                Item = new()
                {
                    Item = theItemFormLink
                }
            });
            newRecipe.WorkbenchKeyword.SetTo(Skyrim.Keyword.CraftingCookpot);

            LoadOrder<IModListingGetter<ISkyrimModGetter>> loadOrder = new()
            {
                new ModListing<ISkyrimMod>(masterMod, true),
                new ModListing<ISkyrimMod>(patchMod, true)
            };

            ILinkCache<ISkyrimMod, ISkyrimModGetter> linkCache = loadOrder.ToImmutableLinkCache();

            var program = new Program(loadOrder, linkCache, patchMod, GameRelease.SkyrimSE, new());


            program.RunPatch();


            var itemLink = patchMod.FormLists.Single().Items.Single();

            Assert.Equal(theItemFormLink, itemLink.Cast<IIngestibleGetter>());
        }
    }
}
