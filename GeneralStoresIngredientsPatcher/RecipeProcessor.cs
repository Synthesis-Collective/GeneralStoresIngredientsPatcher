using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;

namespace GeneralStoresIngredientsPatcher
{
    public class RecipeProcessor
    {
        public readonly HashSet<IFormLinkGetter<IItemGetter>> SpecificSet;
        public readonly HashSet<IFormLinkGetter<IItemGetter>> AllSet;
        public readonly HashSet<IFormLinkGetter<IItemGetter>> IngredientSet;
        public readonly ILinkCache<ISkyrimMod, ISkyrimModGetter> LinkCache;
        public readonly HashSet<IFormLinkGetter<IItemGetter>> DoNotUnburdenFormKeys;

        public RecipeProcessor(HashSet<IFormLinkGetter<IItemGetter>> specificSet, HashSet<IFormLinkGetter<IItemGetter>> allSet, HashSet<IFormLinkGetter<IItemGetter>> ingredientSet,
            ILinkCache<ISkyrimMod, ISkyrimModGetter> linkCache,
            HashSet<IFormLinkGetter<IItemGetter>> doNotUnburdenFormKeys)
        {
            SpecificSet = specificSet;
            AllSet = allSet;
            IngredientSet = ingredientSet;
            LinkCache = linkCache;
            DoNotUnburdenFormKeys = doNotUnburdenFormKeys;
        }

        public void ClassifyRecipeIngredients(IConstructibleObjectGetter cobj)
        {
            var items = cobj.Items;
            if (items == null) return;

            foreach (var item in items)
                ClassifyIngredient(item.Item.Item);
        }

        public void ClassifyIngredient(IFormLinkGetter<IItemGetter> itemLink)
        {
            if (DoNotUnburdenFormKeys.Contains(itemLink)) return;
            if (!itemLink.TryResolve(LinkCache, out var record)) return;

            if (record is IIngredientGetter || record is IIngestibleGetter)
                IngredientSet.Add(itemLink);

            if (ShouldUnburden(record))
            {
                AllSet.Add(itemLink);
                SpecificSet.Add(itemLink);
            }
        }

        public static bool ShouldUnburden(IItemGetter record) => record switch
        {
            IIngredientGetter or IIngestibleGetter or IWeaponGetter => false,
            IArmorGetter armor => IsRingShank(armor),
            _ => true,
        };

        private static bool IsRingShank(IArmorGetter armor) => (armor.EditorID?.Contains("Ring Shank")) == true;
    }
}
