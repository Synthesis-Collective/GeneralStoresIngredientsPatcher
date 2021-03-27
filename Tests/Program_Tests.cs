using GeneralStoresIngredientsPatcher;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
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

            LoadOrder<IModListing<ISkyrimModGetter>> loadOrder = new()
            {
                new ModListing<ISkyrimMod>(patchMod, true)
            };

            ILinkCache<ISkyrimMod, ISkyrimModGetter> linkCache = loadOrder.ToImmutableLinkCache();

            var program = new Program(loadOrder, linkCache, patchMod, GameRelease.SkyrimSE, new());

            program.RunPatch();
        }
    }
}
