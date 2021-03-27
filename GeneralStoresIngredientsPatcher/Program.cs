using Mutagen.Bethesda;
using Mutagen.Bethesda.FormKeys.SkyrimLE;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralStores = Mutagen.Bethesda.FormKeys.SkyrimSE.GeneralStores;

namespace GeneralStoresIngredientsPatcher
{
    public class Program
    {
        static Lazy<Settings> Settings = null!;

        private readonly LoadOrder<IModListing<ISkyrimModGetter>> LoadOrder;
        private readonly ILinkCache<ISkyrimMod, ISkyrimModGetter> LinkCache;
        private readonly ISkyrimMod PatchMod;
        private readonly GameRelease GameRelease;
        private readonly Comparer<FormKey> loadOrderComparer;

        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetAutogeneratedSettings(
                    nickname: "Settings",
                    path: "settings.json",
                    out Settings)
                .SetTypicalOpen(GameRelease.SkyrimSE, "GeneralStoresIngredientsPatcher.esp")
                .Run(args);
        }

        public static readonly ISet<IFormLinkGetter<IKeywordGetter>> workbenchFilter = new HashSet<IFormLinkGetter<IKeywordGetter>>(){
            Skyrim.Keyword.CraftingSmelter,
            Skyrim.Keyword.CraftingSmithingForge,
            Skyrim.Keyword.CraftingCookpot,
            Skyrim.Keyword.CraftingTanningRack,
            Skyrim.Keyword.isGrainMill,
            HearthFires.Keyword.BYOHCraftingOven,
            HearthFires.Keyword.BYOHCarpenterTable
        };

        public static readonly ISet<IFormLinkGetter<IKeywordGetter>> workbenchesThatNeedNoItems = new HashSet<IFormLinkGetter<IKeywordGetter>>(){
            Skyrim.Keyword.CraftingCookpot,
            Skyrim.Keyword.isGrainMill,
            HearthFires.Keyword.BYOHCraftingOven
        };

        private static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            new Program(loadOrder: state.LoadOrder,
                linkCache: state.LinkCache,
                patchMod: state.PatchMod,
                gameRelease: state.GameRelease).RunPatch();
        }

        public readonly HashSet<IFormLinkGetter<IItemGetter>> allSmithingSet = new();
        public readonly HashSet<IFormLinkGetter<IItemGetter>> smeltingSet = new();
        public readonly HashSet<IFormLinkGetter<IItemGetter>> smithingSet = new();
        public readonly HashSet<IFormLinkGetter<IItemGetter>> tanningSet = new();

        public readonly HashSet<IFormLinkGetter<IItemGetter>> hearthFiresConstructionSet = new();
        public readonly HashSet<IFormLinkGetter<IItemGetter>> hearthFiresIngredientSet = new();

        public readonly HashSet<IFormLinkGetter<IItemGetter>> alchemyAndCookingSet = new();
        public readonly HashSet<IFormLinkGetter<IItemGetter>> alchemyAndSmithingSet = new();

        // VendorItemFoodRaw / VendorItemFood
        public readonly HashSet<IFormLinkGetter<IItemGetter>> allFoodSet = new();
        public readonly HashSet<IFormLinkGetter<IItemGetter>> cookedFoodSet = new();
        public readonly HashSet<IFormLinkGetter<IItemGetter>> rawFoodSet = new();

        private readonly Dictionary<IFormLinkGetter<IKeywordGetter>, RecipeProcessor> recipeProcessorForWorkbench = new();

        public Program(LoadOrder<IModListing<ISkyrimModGetter>> loadOrder, ILinkCache<ISkyrimMod, ISkyrimModGetter> linkCache, ISkyrimMod patchMod, GameRelease gameRelease, Settings? settings = null)
        {
            LoadOrder = loadOrder;
            LinkCache = linkCache;
            PatchMod = patchMod;
            GameRelease = gameRelease;

            if (settings != null)
                Settings = new(settings);

            loadOrderComparer = FormKey.LoadOrderComparer(LoadOrder);
        }

        public void RunPatch()
        {
            ClassifyRecipeItems(Settings.Value.DoNotUnburdenList);

            RecordClassifiedItems();
        }

        public void ClassifyRecipeItems(HashSet<IFormLinkGetter<IItemGetter>> doNotUnburdenFormKeys)
        {
            Console.WriteLine("Finding ingredients and results that we don't want to be burdened with.");

            //var doNotUnburdenFormKeys = Settings.Value.DoNotUnburdenList;

            var rpfw = recipeProcessorForWorkbench;

            rpfw[Skyrim.Keyword.CraftingSmelter] = new(smeltingSet, allSmithingSet, alchemyAndSmithingSet, LinkCache, doNotUnburdenFormKeys);
            rpfw[Skyrim.Keyword.CraftingSmithingForge] = new(smithingSet, allSmithingSet, alchemyAndSmithingSet, LinkCache, doNotUnburdenFormKeys);
            rpfw[Skyrim.Keyword.CraftingTanningRack] = new(tanningSet, allSmithingSet, alchemyAndSmithingSet, LinkCache, doNotUnburdenFormKeys);
            rpfw[HearthFires.Keyword.BYOHCarpenterTable] = new(hearthFiresConstructionSet, allSmithingSet, hearthFiresIngredientSet, LinkCache, doNotUnburdenFormKeys);
            rpfw[Skyrim.Keyword.CraftingCookpot] = new(rawFoodSet, allFoodSet, alchemyAndCookingSet, LinkCache, doNotUnburdenFormKeys);
            rpfw[Skyrim.Keyword.isGrainMill] = new(rawFoodSet, allFoodSet, alchemyAndCookingSet, LinkCache, doNotUnburdenFormKeys);
            rpfw[HearthFires.Keyword.BYOHCraftingOven] = new(rawFoodSet, allFoodSet, alchemyAndCookingSet, LinkCache, doNotUnburdenFormKeys);

            foreach (var cobj in LoadOrder.PriorityOrder.ConstructibleObject().WinningOverrides())
            {
                ClassifyRecipeItems(cobj);
            }
        }

        public void ClassifyRecipeItems(IConstructibleObjectGetter recipe)
        {
            if (recipe.WorkbenchKeyword is not IFormLinkGetter<IKeywordGetter> workbench) return;

            if (recipe.CreatedObject is IFormLinkGetter<IItemGetter> result)
            {
                if (workbench.Equals(Skyrim.Keyword.CraftingCookpot) || workbench.Equals(HearthFires.Keyword.BYOHCraftingOven))
                {
                    allFoodSet.Add(result);
                    cookedFoodSet.Add(result);
                }
                else if (workbench.Equals(Skyrim.Keyword.isGrainMill))
                {
                    rawFoodSet.Add(result);
                    cookedFoodSet.Add(result);
                }
            }

            if (!recipeProcessorForWorkbench.TryGetValue(workbench, out var recipeProcessor))
                return;

            recipeProcessor.ClassifyRecipeIngredients(recipe);
        }

        public void RecordClassifiedItems()
        {
            Console.WriteLine("Adding found items to GeneralStores Form Lists...");

            ApplySetToFLST(GeneralStores.FormList.xGSxAlchCookFLST, alchemyAndCookingSet);
            ApplySetToFLST(GeneralStores.FormList.xGSxAlchSmithFLST, alchemyAndSmithingSet);

            ApplySetToFLST(GeneralStores.FormList.xGSxFoodAllFLST, allFoodSet);
            ApplySetToFLST(GeneralStores.FormList.xGSxFoodCookedFLST, cookedFoodSet);
            ApplySetToFLST(GeneralStores.FormList.xGSxFoodRawFLST, rawFoodSet);

            ApplySetToFLST(GeneralStores.FormList.xGSxAllSmithFLST, allSmithingSet);
            ApplySetToFLST(GeneralStores.FormList.xGSxSmeltingFLST, smeltingSet);
            ApplySetToFLST(GeneralStores.FormList.xGSxSmithingFLST, smithingSet);
            ApplySetToFLST(GeneralStores.FormList.xGSxTanningFLST, tanningSet);

            // these FormLists have been merged into GeneralStores.esm in SkyrimSE/VR, but are in a separate plugin in SkyrimLE.
            ApplySetToFLST(GameRelease switch
            {
                GameRelease.SkyrimSE or GameRelease.SkyrimVR => GeneralStores.FormList.xHFSxConstructionFLST,
                GameRelease.SkyrimLE => HearthFireStores_GS.FormList.xHFSxConstructionFLST,
                _ => throw new NotImplementedException()
            }, hearthFiresConstructionSet);

            ApplySetToFLST(GameRelease switch
            {
                GameRelease.SkyrimSE or GameRelease.SkyrimVR => GeneralStores.FormList.xHFSxIngredientsFLST,
                GameRelease.SkyrimLE => HearthFireStores_GS.FormList.xHFSxIngredientsFLST,
                _ => throw new NotImplementedException()
            }, hearthFiresIngredientSet);
        }

        public void ApplySetToFLST(IFormLinkGetter<IFormListGetter> flstKey, ISet<IFormLinkGetter<IItemGetter>> set)
        {
            if (!flstKey.TryResolve(LinkCache, out var flst)) return;

            var flstEDID = flst.EditorID;

            HashSet<IFormLinkGetter<IItemGetter>>? missingSet = new(set.Count);

            Console.WriteLine($"Found {set.Count} records that should be in {flstEDID}");

            foreach (var thing in flst.Items)
                if (thing is IFormLinkGetter<IItemGetter> item)
                    if (!set.Remove(item))
                        missingSet.Add(item);

            if (missingSet.Count > 0)
            {
                Console.WriteLine($"The following {missingSet.Count} records in {flstEDID} were not in the records we found:");
                foreach (var item in missingSet.OrderBy(x => x.FormKey, loadOrderComparer))
                    Console.WriteLine($"    {item}");
            }

            Console.WriteLine($"Found {set.Count} new records to add to {flstEDID}");

            if (set.Count == 0) return;

            var modifiedFlst = PatchMod.FormLists.GetOrAddAsOverride(flst);

            modifiedFlst.Items.AddRange(set.OrderBy(x => x.FormKey, loadOrderComparer));
        }
    }
}
