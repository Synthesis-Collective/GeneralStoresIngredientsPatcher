using GeneralStoresIngredientsPatcher;
using Mutagen.Bethesda;
using Mutagen.Bethesda.FormKeys.SkyrimLE;
using Mutagen.Bethesda.Skyrim;
using System;
using System.Linq;
using Xunit;
using GeneralStores = Mutagen.Bethesda.FormKeys.SkyrimSE.GeneralStores;

namespace Tests
{
    public class RecordClassifiedItems_Tests
    {
        public static readonly ModKey masterModKey = ModKey.FromNameAndExtension("master.esp");
        public static readonly ModKey patchModKey = ModKey.FromNameAndExtension("patch.esp");

        [Fact]
        public void AddAThing()
        {
            var generalStores = new SkyrimMod(GeneralStores.ModKey, SkyrimRelease.SkyrimSE);

            var xGSxFoodAllFLST = new FormList(GeneralStores.FormList.xGSxFoodAllFLST.FormKey, SkyrimRelease.SkyrimSE);

            generalStores.FormLists.Add(xGSxFoodAllFLST);

            SkyrimMod masterMod = new(masterModKey, SkyrimRelease.SkyrimSE);

            var newThing = masterMod.MiscItems.AddNew("newItem");

            SkyrimMod patchMod = new(patchModKey, SkyrimRelease.SkyrimSE);

            var loadOrder = new LoadOrder<IModListing<ISkyrimModGetter>>
            {
                new ModListing<ISkyrimModGetter>(generalStores, true),
                new ModListing<ISkyrimModGetter>(masterMod, true),
                new ModListing<ISkyrimModGetter>(patchMod, true)
            };

            var linkCache = loadOrder.ToImmutableLinkCache();

            Program program = new(loadOrder, linkCache, patchMod, GameRelease.SkyrimSE);

            program.allFoodSet.Add(newThing.AsLink());

            program.RecordClassifiedItems();

            Assert.Single(patchMod.FormLists);

            var updatedFlst = patchMod.FormLists.Single();

            Assert.Single(updatedFlst.Items);

            var newThingLink = updatedFlst.Items.Single();

            Assert.False(newThingLink.IsNull);

            Assert.Equal(newThing.AsLink(), newThingLink);
        }

        [Theory]
        [InlineData(SkyrimRelease.SkyrimLE)]
        [InlineData(SkyrimRelease.SkyrimSE)]
        [InlineData(SkyrimRelease.SkyrimVR)]
        public void AddAHearthFiresThing(SkyrimRelease release)
        {
            var xHFSxConstructionFLSTFormLink = release switch
            {
                SkyrimRelease.SkyrimLE => HearthFireStores_GS.FormList.xHFSxConstructionFLST,
                SkyrimRelease.SkyrimSE or SkyrimRelease.SkyrimVR => GeneralStores.FormList.xHFSxConstructionFLST,
                _ => throw new ArgumentException(null, nameof(release)),
            };

            var gameRelease = release switch
            {
                SkyrimRelease.SkyrimLE => GameRelease.SkyrimLE,
                SkyrimRelease.SkyrimSE => GameRelease.SkyrimSE,
                SkyrimRelease.SkyrimVR => GameRelease.SkyrimVR,
                _ => throw new ArgumentException(null, nameof(release)),
            };

            var generalStores = new SkyrimMod(xHFSxConstructionFLSTFormLink.FormKey.ModKey, release);

            var xHFSxConstructionFLST = new FormList(xHFSxConstructionFLSTFormLink.FormKey, release);

            generalStores.FormLists.Add(xHFSxConstructionFLST);

            SkyrimMod masterMod = new(masterModKey, release);

            var newThing = masterMod.MiscItems.AddNew("newItem");

            SkyrimMod patchMod = new(patchModKey, release);

            var loadOrder = new LoadOrder<IModListing<ISkyrimModGetter>>
            {
                new ModListing<ISkyrimModGetter>(generalStores, true),
                new ModListing<ISkyrimModGetter>(masterMod, true),
                new ModListing<ISkyrimModGetter>(patchMod, true)
            };

            var linkCache = loadOrder.ToImmutableLinkCache();

            Program program = new(loadOrder, linkCache, patchMod, gameRelease);

            program.hearthFiresConstructionSet.Add(newThing.AsLink());

            program.RecordClassifiedItems();

            Assert.Single(patchMod.FormLists);

            var updatedFlst = patchMod.FormLists.Single();

            Assert.Equal(xHFSxConstructionFLSTFormLink, updatedFlst.AsLink());

            Assert.Single(updatedFlst.Items);

            var newThingLink = updatedFlst.Items.Single();

            Assert.False(newThingLink.IsNull);

            Assert.Equal(newThing.AsLink(), newThingLink);
        }
    }
}
