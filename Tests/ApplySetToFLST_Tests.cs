using GeneralStoresIngredientsPatcher;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class ApplySetToFLST_Tests
    {
        public static readonly ModKey masterModKey = ModKey.FromNameAndExtension("master.esp");
        public static readonly ModKey patchModKey = ModKey.FromNameAndExtension("patch.esp");

        [Fact]
        public void AddNothing()
        {
            SkyrimMod masterMod = new(masterModKey, SkyrimRelease.SkyrimSE);

            var origFlst = masterMod.FormLists.AddNew("origFlst");

            var newThing = masterMod.MiscItems.AddNew("newItem");

            origFlst.Items.Add(newThing.AsLink());

            SkyrimMod patchMod = new(patchModKey, SkyrimRelease.SkyrimSE);

            var linkCache = masterMod.ToImmutableLinkCache();

            var loadOrder = new LoadOrder<IModListing<ISkyrimModGetter>>
            {
                new ModListing<ISkyrimModGetter>(masterMod, true),
                new ModListing<ISkyrimModGetter>(patchMod, true)
            };

            Program program = new(loadOrder, linkCache, patchMod, GameRelease.SkyrimSE);

            HashSet<IFormLinkGetter<IItemGetter>> set = new();

            program.ApplySetToFLST(origFlst.AsLinkGetter(), set);

            Assert.Empty(patchMod.FormLists);
        }

        [Fact]
        public void AddAThing()
        {
            SkyrimMod masterMod = new(masterModKey, SkyrimRelease.SkyrimSE);

            var origFlst = masterMod.FormLists.AddNew("origFlst");

            var newThing = masterMod.MiscItems.AddNew("newItem");

            SkyrimMod patchMod = new(patchModKey, SkyrimRelease.SkyrimSE);

            var linkCache = masterMod.ToImmutableLinkCache();

            var loadOrder = new LoadOrder<IModListing<ISkyrimModGetter>>
            {
                new ModListing<ISkyrimModGetter>(masterMod, true),
                new ModListing<ISkyrimModGetter>(patchMod, true)
            };

            Program program = new(loadOrder, linkCache, patchMod, GameRelease.SkyrimSE);

            HashSet<IFormLinkGetter<IItemGetter>> set = new();

            set.Add(newThing.AsLink());

            program.ApplySetToFLST(origFlst.AsLinkGetter(), set);

            Assert.Single(patchMod.FormLists);

            var updatedFlst = patchMod.FormLists.Single();

            Assert.Single(updatedFlst.Items);

            var newThingLink = updatedFlst.Items.Single();

            Assert.False(newThingLink.IsNull);

            Assert.Equal(newThing.AsLink(), newThingLink);
        }

        [Fact]
        public void AddAnExistingThing()
        {
            SkyrimMod masterMod = new(masterModKey, SkyrimRelease.SkyrimSE);

            var origFlst = masterMod.FormLists.AddNew("origFlst");

            var newThing = masterMod.MiscItems.AddNew("newItem");

            origFlst.Items.Add(newThing.AsLink());

            SkyrimMod patchMod = new(patchModKey, SkyrimRelease.SkyrimSE);

            var linkCache = masterMod.ToImmutableLinkCache();

            var loadOrder = new LoadOrder<IModListing<ISkyrimModGetter>>
            {
                new ModListing<ISkyrimModGetter>(masterMod, true),
                new ModListing<ISkyrimModGetter>(patchMod, true)
            };

            Program program = new(loadOrder, linkCache, patchMod, GameRelease.SkyrimSE);

            HashSet<IFormLinkGetter<IItemGetter>> set = new();

            set.Add(newThing.AsLink());

            program.ApplySetToFLST(origFlst.AsLinkGetter(), set);

            Assert.Empty(patchMod.FormLists);
        }
    }
}
