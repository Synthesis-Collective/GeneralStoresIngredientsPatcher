#pragma warning disable IDE1006 // Naming Styles
namespace Mutagen.Bethesda.FormKeys.SkyrimLE
{
    public static partial class HearthFireStores_GS
    {
        public static class FormList
        {
            private readonly static ModKey ModKey = ModKey.FromNameAndExtension("HearthFireStores(GS).esp");
            public static FormKey xHFSxConstructionFLST => ModKey.MakeFormKey(0x12c7);
            public static FormKey xHFSxIngredientsFLST => ModKey.MakeFormKey(0x840d);
        }
    }
}
