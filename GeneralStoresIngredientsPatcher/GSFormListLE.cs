#pragma warning disable IDE1006 // Naming Styles
using Mutagen.Bethesda.Skyrim;

namespace Mutagen.Bethesda.FormKeys.SkyrimLE
{
    public static partial class GeneralStores
    {
        public readonly static ModKey ModKey = ModKey.FromNameAndExtension("GeneralStores.esm");
        public static class FormList
        {
            private static FormLink<IKeywordGetter> Construct(uint formID) => new(ModKey.MakeFormKey(formID));
            public static FormKey xGSxAlchCookFLST => Construct(0x10193);
            public static FormKey xGSxAlchCookNewFLST => Construct(0x9e6ec);
            public static FormKey xGSxAlchSmithFLST => Construct(0x8508a);
            public static FormKey xGSxAllSmithFLST => Construct(0xa6db9);
            public static FormKey xGSxAllSmithNewFLST => Construct(0xa6dbb);
            public static FormKey xGSxArmorAllFLST => Construct(0xa7de5);
            public static FormKey xGSxArmorAllNewFLST => Construct(0xa6dba);
            public static FormKey xGSxArmorClothingFLST => Construct(0xa3d2b);
            public static FormKey xGSxArmorClothingNewFLST => Construct(0xa47fb);
            public static FormKey xGSxArmorHeavyFLST => Construct(0xa3d2c);
            public static FormKey xGSxArmorHeavyNewFLST => Construct(0xa47f9);
            public static FormKey xGSxArmorLightFLST => Construct(0xa3d2e);
            public static FormKey xGSxArmorLightNewFLST => Construct(0xa47fa);
            public static FormKey xGSxArmorShieldsFLST => Construct(0xa3d2f);
            public static FormKey xGSxArmorShieldsNewFLST => Construct(0xa47f8);
            public static FormKey xGSxBooksFLST => Construct(0x7f4de);
            public static FormKey xGSxBooksNewFLST => Construct(0x9f716);
            public static FormKey xGSxDragonClawsFLST => Construct(0x8198);
            public static FormKey xGSxFoodAllFLST => Construct(0xa6dbc);
            public static FormKey xGSxFoodAllNewFLST => Construct(0xa6dbd);
            public static FormKey xGSxFoodCookedFLST => Construct(0x13c61);
            public static FormKey xGSxFoodCookedNewFLST => Construct(0x99e94);
            public static FormKey xGSxFoodRawFLST => Construct(0x13c60);
            public static FormKey xGSxFoodRawNewFLST => Construct(0x99e95);
            public static FormKey xGSxGemstonesFLST => Construct(0x138cb);
            public static FormKey xGSxIngredientFLST => Construct(0x6027);
            public static FormKey xGSxIngredientsNewFLST => Construct(0x9e6eb);
            public static FormKey xGSxMorterMaterialsFLST => Construct(0xcb9a5);
            public static FormKey xGSxPotionsAllFLST => Construct(0xa6dbe);
            public static FormKey xGSxPotionsAllNewFLST => Construct(0x978db);
            public static FormKey xGSxPotionsBadFLST => Construct(0x92d7a);
            public static FormKey xGSxPotionsBadNewFLST => Construct(0x978dd);
            public static FormKey xGSxPotionsGoodFLST => Construct(0x92d7b);
            public static FormKey xGSxPotionsGoodNewFLST => Construct(0x978dc);
            public static FormKey xGSxRestorativesFLST => Construct(0xc68e2);
            public static FormKey xGSxRestorativesNewFLST => Construct(0x9c93);
            public static FormKey xGSxScrollsFLST => Construct(0x7f4dd);
            public static FormKey xGSxScrollsNewFLST => Construct(0x9f717);
            public static FormKey xGSxSmeltingFLST => Construct(0x1018b);
            public static FormKey xGSxSmeltingNewFLST => Construct(0x9a966);
            public static FormKey xGSxSmithingFLST => Construct(0x6026);
            public static FormKey xGSxSmithingNewFLST => Construct(0x9a965);
            public static FormKey xGSxSoulGemALLFLST => Construct(0x1018f);
            public static FormKey xGSxSoulGemEmptyFLST => Construct(0x10191);
            public static FormKey xGSxSoulGemFilledFLST => Construct(0x6029);
            public static FormKey xGSxSoulGemFragsFLST => Construct(0x66b2);
            public static FormKey xGSxSoulGemGrandFLST => Construct(0x10190);
            public static FormKey xGSxSpellTomesFLST => Construct(0x9f714);
            public static FormKey xGSxSpellTomesNewFLST => Construct(0x9f718);
            public static FormKey xGSxTanningFLST => Construct(0x8c714);
            public static FormKey xGSxTanningNewFLST => Construct(0x9a967);
            public static FormKey xGSxTreasuresFLST => Construct(0x9e6ed);
            public static FormKey xGSxWeaponArcheryFLST => Construct(0xb35c2);
            public static FormKey xGSxWeaponArcheryNewFLST => Construct(0xb35c3);
            public static FormKey xGSxWeaponOneHandedFLST => Construct(0xb35c1);
            public static FormKey xGSxWeaponOneHandedNewFLST => Construct(0xb35c4);
            public static FormKey xGSxWeaponsAllFLST => Construct(0xb915f);
            public static FormKey xGSxWeaponsNewFLST => Construct(0xb9160);
            public static FormKey xGSxWeaponStaffFLST => Construct(0xb35c7);
            public static FormKey xGSxWeaponStaffNewFLST => Construct(0xb35c6);
            public static FormKey xGSxWeaponTwoHandedFLST => Construct(0xb35c0);
            public static FormKey xGSxWeaponTwoHandedNewFLST => Construct(0xb35c5);
        }
    }
}
