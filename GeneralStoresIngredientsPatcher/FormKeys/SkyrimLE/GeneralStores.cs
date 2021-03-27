using Mutagen.Bethesda.Skyrim;
using System.CodeDom.Compiler;

namespace Mutagen.Bethesda.FormKeys.SkyrimLE
{
    [GeneratedCode("Mutagen.Bethesda.FormKeys.Generator", "2.0.0")]
    public static partial class GeneralStores
    {
        public readonly static ModKey ModKey = ModKey.FromNameAndExtension("GeneralStores.esm");
        public static class FormList
        {
            private static FormLink<IFormListGetter> Construct(uint formID) => new(ModKey.MakeFormKey(formID));
            public static IFormLink<IFormListGetter> xGSxAlchCookFLST => Construct(0x10193);
            public static IFormLink<IFormListGetter> xGSxAlchCookNewFLST => Construct(0x9e6ec);
            public static IFormLink<IFormListGetter> xGSxAlchSmithFLST => Construct(0x8508a);
            public static IFormLink<IFormListGetter> xGSxAllSmithFLST => Construct(0xa6db9);
            public static IFormLink<IFormListGetter> xGSxAllSmithNewFLST => Construct(0xa6dbb);
            public static IFormLink<IFormListGetter> xGSxArmorAllFLST => Construct(0xa7de5);
            public static IFormLink<IFormListGetter> xGSxArmorAllNewFLST => Construct(0xa6dba);
            public static IFormLink<IFormListGetter> xGSxArmorClothingFLST => Construct(0xa3d2b);
            public static IFormLink<IFormListGetter> xGSxArmorClothingNewFLST => Construct(0xa47fb);
            public static IFormLink<IFormListGetter> xGSxArmorHeavyFLST => Construct(0xa3d2c);
            public static IFormLink<IFormListGetter> xGSxArmorHeavyNewFLST => Construct(0xa47f9);
            public static IFormLink<IFormListGetter> xGSxArmorLightFLST => Construct(0xa3d2e);
            public static IFormLink<IFormListGetter> xGSxArmorLightNewFLST => Construct(0xa47fa);
            public static IFormLink<IFormListGetter> xGSxArmorShieldsFLST => Construct(0xa3d2f);
            public static IFormLink<IFormListGetter> xGSxArmorShieldsNewFLST => Construct(0xa47f8);
            public static IFormLink<IFormListGetter> xGSxBooksFLST => Construct(0x7f4de);
            public static IFormLink<IFormListGetter> xGSxBooksNewFLST => Construct(0x9f716);
            public static IFormLink<IFormListGetter> xGSxDragonClawsFLST => Construct(0x8198);
            public static IFormLink<IFormListGetter> xGSxFoodAllFLST => Construct(0xa6dbc);
            public static IFormLink<IFormListGetter> xGSxFoodAllNewFLST => Construct(0xa6dbd);
            public static IFormLink<IFormListGetter> xGSxFoodCookedFLST => Construct(0x13c61);
            public static IFormLink<IFormListGetter> xGSxFoodCookedNewFLST => Construct(0x99e94);
            public static IFormLink<IFormListGetter> xGSxFoodRawFLST => Construct(0x13c60);
            public static IFormLink<IFormListGetter> xGSxFoodRawNewFLST => Construct(0x99e95);
            public static IFormLink<IFormListGetter> xGSxGemstonesFLST => Construct(0x138cb);
            public static IFormLink<IFormListGetter> xGSxIngredientFLST => Construct(0x6027);
            public static IFormLink<IFormListGetter> xGSxIngredientsNewFLST => Construct(0x9e6eb);
            public static IFormLink<IFormListGetter> xGSxMorterMaterialsFLST => Construct(0xcb9a5);
            public static IFormLink<IFormListGetter> xGSxPotionsAllFLST => Construct(0xa6dbe);
            public static IFormLink<IFormListGetter> xGSxPotionsAllNewFLST => Construct(0x978db);
            public static IFormLink<IFormListGetter> xGSxPotionsBadFLST => Construct(0x92d7a);
            public static IFormLink<IFormListGetter> xGSxPotionsBadNewFLST => Construct(0x978dd);
            public static IFormLink<IFormListGetter> xGSxPotionsGoodFLST => Construct(0x92d7b);
            public static IFormLink<IFormListGetter> xGSxPotionsGoodNewFLST => Construct(0x978dc);
            public static IFormLink<IFormListGetter> xGSxRestorativesFLST => Construct(0xc68e2);
            public static IFormLink<IFormListGetter> xGSxRestorativesNewFLST => Construct(0x9c93);
            public static IFormLink<IFormListGetter> xGSxScrollsFLST => Construct(0x7f4dd);
            public static IFormLink<IFormListGetter> xGSxScrollsNewFLST => Construct(0x9f717);
            public static IFormLink<IFormListGetter> xGSxSmeltingFLST => Construct(0x1018b);
            public static IFormLink<IFormListGetter> xGSxSmeltingNewFLST => Construct(0x9a966);
            public static IFormLink<IFormListGetter> xGSxSmithingFLST => Construct(0x6026);
            public static IFormLink<IFormListGetter> xGSxSmithingNewFLST => Construct(0x9a965);
            public static IFormLink<IFormListGetter> xGSxSoulGemALLFLST => Construct(0x1018f);
            public static IFormLink<IFormListGetter> xGSxSoulGemEmptyFLST => Construct(0x10191);
            public static IFormLink<IFormListGetter> xGSxSoulGemFilledFLST => Construct(0x6029);
            public static IFormLink<IFormListGetter> xGSxSoulGemFragsFLST => Construct(0x66b2);
            public static IFormLink<IFormListGetter> xGSxSoulGemGrandFLST => Construct(0x10190);
            public static IFormLink<IFormListGetter> xGSxSpellTomesFLST => Construct(0x9f714);
            public static IFormLink<IFormListGetter> xGSxSpellTomesNewFLST => Construct(0x9f718);
            public static IFormLink<IFormListGetter> xGSxTanningFLST => Construct(0x8c714);
            public static IFormLink<IFormListGetter> xGSxTanningNewFLST => Construct(0x9a967);
            public static IFormLink<IFormListGetter> xGSxTreasuresFLST => Construct(0x9e6ed);
            public static IFormLink<IFormListGetter> xGSxWeaponArcheryFLST => Construct(0xb35c2);
            public static IFormLink<IFormListGetter> xGSxWeaponArcheryNewFLST => Construct(0xb35c3);
            public static IFormLink<IFormListGetter> xGSxWeaponOneHandedFLST => Construct(0xb35c1);
            public static IFormLink<IFormListGetter> xGSxWeaponOneHandedNewFLST => Construct(0xb35c4);
            public static IFormLink<IFormListGetter> xGSxWeaponsAllFLST => Construct(0xb915f);
            public static IFormLink<IFormListGetter> xGSxWeaponsNewFLST => Construct(0xb9160);
            public static IFormLink<IFormListGetter> xGSxWeaponStaffFLST => Construct(0xb35c7);
            public static IFormLink<IFormListGetter> xGSxWeaponStaffNewFLST => Construct(0xb35c6);
            public static IFormLink<IFormListGetter> xGSxWeaponTwoHandedFLST => Construct(0xb35c0);
            public static IFormLink<IFormListGetter> xGSxWeaponTwoHandedNewFLST => Construct(0xb35c5);
        }
    }
}
