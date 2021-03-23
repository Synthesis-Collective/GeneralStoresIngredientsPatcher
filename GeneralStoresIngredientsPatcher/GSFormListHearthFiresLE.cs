#pragma warning disable IDE1006 // Naming Styles
using Mutagen.Bethesda.Skyrim;

namespace Mutagen.Bethesda.FormKeys.SkyrimLE
{
    public static partial class HearthFireStores_GS
    {
        public readonly static ModKey ModKey = ModKey.FromNameAndExtension("HearthFireStores(GS).esp");
        public static class FormList
        {
            private static FormLink<IFormListGetter> Construct(uint formID) => new(ModKey.MakeFormKey(formID));
            public static IFormLink<IFormListGetter> xHFSxConstructionFLST => Construct(0x12c7);
            public static IFormLink<IFormListGetter> xHFSxIngredientsFLST => Construct(0x840d);
        }
    }
}
