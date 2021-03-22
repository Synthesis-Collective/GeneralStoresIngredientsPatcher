using Mutagen.Bethesda;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Skyrim;
using System.Collections.Generic;

namespace GeneralStoresIngredientsPatcher
{
    public record Settings
    {
        public HashSet<IFormLinkGetter<IItemGetter>> DoNotUnburdenList = new(){
            Skyrim.MiscItem.Lockpick,
            Skyrim.MiscItem.Gold001,
            Skyrim.Light.Torch01
        };
    }
}