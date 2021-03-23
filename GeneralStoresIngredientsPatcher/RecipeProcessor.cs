using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using System.Collections.Generic;

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
            if (!itemLink.TryResolve(LinkCache, out var record)) return;

            bool shouldUnburden;
            switch (record)
            {
                case IIngredientGetter:
                case IIngestibleGetter:
                    IngredientSet.Add(itemLink);
                    shouldUnburden = false;
                    break;
                case IArmorGetter armor:
                    if (armor.EditorID?.Contains("Ring Shank") == true)
                        shouldUnburden = true; // intermediate ingredient from Immersive Jewellery
                    else
                        shouldUnburden = false;
                    break;
                case IWeaponGetter:
                    shouldUnburden = false;
                    break;
                default:
                    shouldUnburden = true;
                    break;
            }

            if (DoNotUnburdenFormKeys.Contains(itemLink))
                shouldUnburden = false;

            if (shouldUnburden)
            {
                AllSet.Add(itemLink);
                SpecificSet.Add(itemLink);
            }
        }
    }
}
