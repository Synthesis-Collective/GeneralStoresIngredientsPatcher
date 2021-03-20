#pragma warning disable IDE1006 // Naming Styles
using Mutagen.Bethesda.Skyrim;

namespace Mutagen.Bethesda.FormKeys.SkyrimSE
{
    public static partial class GeneralStores
    {
        public readonly static ModKey ModKey = ModKey.FromNameAndExtension("GeneralStores.esm");
        public static class FormList
        {
            private static FormLink<IFormListGetter> Construct(uint formID) => new(ModKey.MakeFormKey(formID));
            public static FormLink<IFormListGetter> GeneralStoresAllDLCAddonForms => Construct(0xcf37e);
            public static FormLink<IFormListGetter> GeneralStoresForms => Construct(0xcf37d);
            public static FormLink<IFormListGetter> xGSxAlchCookFLST => Construct(0x10193);
            public static FormLink<IFormListGetter> xGSxAlchCookNewFLST => Construct(0x9e6ec);
            public static FormLink<IFormListGetter> xGSxAlchSmithFLST => Construct(0x8508a);
            public static FormLink<IFormListGetter> xGSxAllSmithFLST => Construct(0xa6db9);
            public static FormLink<IFormListGetter> xGSxAllSmithNewFLST => Construct(0xa6dbb);
            public static FormLink<IFormListGetter> xGSxArmorAllFLST => Construct(0xa7de5);
            public static FormLink<IFormListGetter> xGSxArmorAllNewFLST => Construct(0xa6dba);
            public static FormLink<IFormListGetter> xGSxArmorClothingFLST => Construct(0xa3d2b);
            public static FormLink<IFormListGetter> xGSxArmorClothingNewFLST => Construct(0xa47fb);
            public static FormLink<IFormListGetter> xGSxArmorHeavyFLST => Construct(0xa3d2c);
            public static FormLink<IFormListGetter> xGSxArmorHeavyNewFLST => Construct(0xa47f9);
            public static FormLink<IFormListGetter> xGSxArmorLightFLST => Construct(0xa3d2e);
            public static FormLink<IFormListGetter> xGSxArmorLightNewFLST => Construct(0xa47fa);
            public static FormLink<IFormListGetter> xGSxArmorShieldsFLST => Construct(0xa3d2f);
            public static FormLink<IFormListGetter> xGSxArmorShieldsNewFLST => Construct(0xa47f8);
            public static FormLink<IFormListGetter> xGSxBooksFLST => Construct(0x7f4de);
            public static FormLink<IFormListGetter> xGSxBooksNewFLST => Construct(0x9f716);
            public static FormLink<IFormListGetter> xGSxDragonClawsFLST => Construct(0x8198);
            public static FormLink<IFormListGetter> xGSxFoodAllFLST => Construct(0xa6dbc);
            public static FormLink<IFormListGetter> xGSxFoodAllNewFLST => Construct(0xa6dbd);
            public static FormLink<IFormListGetter> xGSxFoodCookedFLST => Construct(0x13c61);
            public static FormLink<IFormListGetter> xGSxFoodCookedNewFLST => Construct(0x99e94);
            public static FormLink<IFormListGetter> xGSxFoodRawFLST => Construct(0x13c60);
            public static FormLink<IFormListGetter> xGSxFoodRawNewFLST => Construct(0x99e95);
            public static FormLink<IFormListGetter> xGSxGemstonesFLST => Construct(0x138cb);
            public static FormLink<IFormListGetter> xGSxIngredientFLST => Construct(0x6027);
            public static FormLink<IFormListGetter> xGSxIngredientsNewFLST => Construct(0x9e6eb);
            public static FormLink<IFormListGetter> xGSxMorterMaterialsFLST => Construct(0xcb9a5);
            public static FormLink<IFormListGetter> xGSxPotionsAllFLST => Construct(0xa6dbe);
            public static FormLink<IFormListGetter> xGSxPotionsAllNewFLST => Construct(0x978db);
            public static FormLink<IFormListGetter> xGSxPotionsBadFLST => Construct(0x92d7a);
            public static FormLink<IFormListGetter> xGSxPotionsBadNewFLST => Construct(0x978dd);
            public static FormLink<IFormListGetter> xGSxPotionsGoodFLST => Construct(0x92d7b);
            public static FormLink<IFormListGetter> xGSxPotionsGoodNewFLST => Construct(0x978dc);
            public static FormLink<IFormListGetter> xGSxRestorativesFLST => Construct(0xc68e2);
            public static FormLink<IFormListGetter> xGSxRestorativesNewFLST => Construct(0x9c93);
            public static FormLink<IFormListGetter> xGSxScrollsFLST => Construct(0x7f4dd);
            public static FormLink<IFormListGetter> xGSxScrollsNewFLST => Construct(0x9f717);
            public static FormLink<IFormListGetter> xGSxSmeltingFLST => Construct(0x1018b);
            public static FormLink<IFormListGetter> xGSxSmeltingNewFLST => Construct(0x9a966);
            public static FormLink<IFormListGetter> xGSxSmithingFLST => Construct(0x6026);
            public static FormLink<IFormListGetter> xGSxSmithingNewFLST => Construct(0x9a965);
            public static FormLink<IFormListGetter> xGSxSoulGemALLFLST => Construct(0x1018f);
            public static FormLink<IFormListGetter> xGSxSoulGemEmptyFLST => Construct(0x10191);
            public static FormLink<IFormListGetter> xGSxSoulGemFilledFLST => Construct(0x6029);
            public static FormLink<IFormListGetter> xGSxSoulGemFragsFLST => Construct(0x66b2);
            public static FormLink<IFormListGetter> xGSxSoulGemGrandFLST => Construct(0x10190);
            public static FormLink<IFormListGetter> xGSxSpellTomesFLST => Construct(0x9f714);
            public static FormLink<IFormListGetter> xGSxSpellTomesNewFLST => Construct(0x9f718);
            public static FormLink<IFormListGetter> xGSxTanningFLST => Construct(0x8c714);
            public static FormLink<IFormListGetter> xGSxTanningNewFLST => Construct(0x9a967);
            public static FormLink<IFormListGetter> xGSxTreasuresFLST => Construct(0x9e6ed);
            public static FormLink<IFormListGetter> xGSxWeaponArcheryFLST => Construct(0xb35c2);
            public static FormLink<IFormListGetter> xGSxWeaponArcheryNewFLST => Construct(0xb35c3);
            public static FormLink<IFormListGetter> xGSxWeaponOneHandedFLST => Construct(0xb35c1);
            public static FormLink<IFormListGetter> xGSxWeaponOneHandedNewFLST => Construct(0xb35c4);
            public static FormLink<IFormListGetter> xGSxWeaponsAllFLST => Construct(0xb915f);
            public static FormLink<IFormListGetter> xGSxWeaponsNewFLST => Construct(0xb9160);
            public static FormLink<IFormListGetter> xGSxWeaponStaffFLST => Construct(0xb35c7);
            public static FormLink<IFormListGetter> xGSxWeaponStaffNewFLST => Construct(0xb35c6);
            public static FormLink<IFormListGetter> xGSxWeaponTwoHandedFLST => Construct(0xb35c0);
            public static FormLink<IFormListGetter> xGSxWeaponTwoHandedNewFLST => Construct(0xb35c5);
            public static FormLink<IFormListGetter> xHFSxConstructionFLST => Construct(0x12c7);
            public static FormLink<IFormListGetter> xHFSxIngredientsFLST => Construct(0x840d);
        }
    }
}
