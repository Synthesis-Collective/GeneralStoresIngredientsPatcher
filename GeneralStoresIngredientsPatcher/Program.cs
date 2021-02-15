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
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .Run(args, new RunPreferences()
                {
                    ActionsForEmptyArgs = new RunDefaultPatcher()
                    {
                        IdentifyingModKey = "YourPatcher.esp",
                        TargetRelease = GameRelease.SkyrimSE,
                    }
                });
        }

        public static readonly ISet<FormKey> workbenchFilter = new HashSet<FormKey>(){
            Skyrim.Keyword.CraftingSmelter,
            Skyrim.Keyword.CraftingSmithingForge,
            Skyrim.Keyword.CraftingCookpot,
            Skyrim.Keyword.CraftingTanningRack,
            Skyrim.Keyword.isGrainMill,
            HearthFires.Keyword.BYOHCraftingOven,
            HearthFires.Keyword.BYOHCarpenterTable
        };

        public static readonly ISet<FormKey> workbenchesThatNeedNoItems = new HashSet<FormKey>(){
            Skyrim.Keyword.CraftingCookpot,
            Skyrim.Keyword.isGrainMill,
            HearthFires.Keyword.BYOHCraftingOven
        };

        // TODO Add a settings dialog for this.
        public static readonly ISet<FormKey> doNotUnburdenFormKeys = new HashSet<FormKey>(){
            Skyrim.MiscItem.Lockpick,
            Skyrim.MiscItem.Gold001,
            Skyrim.Light.Torch01,
        };

        public static bool goSlower = false;

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            var allSmithingSet = new HashSet<FormKey>();
            var smeltingSet = new HashSet<FormKey>();
            var smithingSet = new HashSet<FormKey>();
            var tanningSet = new HashSet<FormKey>();
            var hearthFiresConstructionSet = new HashSet<FormKey>();

            var hearthFiresIngredientSet = new HashSet<FormKey>();
            var alchemyAndCookingSet = new HashSet<FormKey>();
            var alchemyAndSmithingSet = new HashSet<FormKey>();

            // VendorItemFoodRaw / VendorItemFood
            var allFoodSet = new HashSet<FormKey>();
            var cookedFoodSet = new HashSet<FormKey>();
            var rawFoodSet = new HashSet<FormKey>();

            ISet<FormKey> specificSet = allSmithingSet;
            ISet<FormKey> allSet = smeltingSet;
            ISet<FormKey> ingredientSet = alchemyAndSmithingSet;

            var formListsForWorkbench = new Dictionary<FormKey, Action<IConstructibleObjectGetter>>()
            {
                {Skyrim.Keyword.CraftingSmelter, delegate {
                    ingredientSet = alchemyAndSmithingSet;
                    allSet = allSmithingSet;
                    specificSet = smeltingSet;
                }},
                {Skyrim.Keyword.CraftingSmithingForge, delegate {
                    ingredientSet = alchemyAndSmithingSet;
                    allSet = allSmithingSet;
                    specificSet = smithingSet;
                }},
                {Skyrim.Keyword.CraftingTanningRack, delegate {
                    ingredientSet = alchemyAndSmithingSet;
                    allSet = allSmithingSet;
                    specificSet = tanningSet;
                }},
                {HearthFires.Keyword.BYOHCarpenterTable, delegate {
                    ingredientSet = hearthFiresIngredientSet;
                    allSet = allSmithingSet;
                    specificSet = hearthFiresConstructionSet;
                }},
                {Skyrim.Keyword.CraftingCookpot, delegate(IConstructibleObjectGetter cobj) {
                    if (cobj.CreatedObject.FormKeyNullable is FormKey result) {
                        allFoodSet.Add(result);
                        cookedFoodSet.Add(result);
                    }
                    ingredientSet = alchemyAndCookingSet;
                    allSet = allFoodSet;
                    specificSet = rawFoodSet;
                }},
                {Skyrim.Keyword.isGrainMill, delegate(IConstructibleObjectGetter cobj) {
                    if (cobj.CreatedObject.FormKeyNullable is FormKey result) {
                        rawFoodSet.Add(result);
                        cookedFoodSet.Add(result);
                    }
                    ingredientSet = alchemyAndCookingSet;
                    allSet = allFoodSet;
                    specificSet = rawFoodSet;
                }},
            };

            formListsForWorkbench[HearthFires.Keyword.BYOHCraftingOven] = formListsForWorkbench[Skyrim.Keyword.CraftingCookpot];

            Console.WriteLine("Finding ingredients and results that we don't want to be burdened with.");

            if (!goSlower)
            {
                // this seems to go faster than the other way?
                var ingredients = state.LoadOrder.PriorityOrder.Ingredient().WinningOverrides().Select(x => x.FormKey);
                var ingestibles = state.LoadOrder.PriorityOrder.Ingestible().WinningOverrides().Select(x => x.FormKey);
                var ingredientsOrIngestibles = ingredients.Union(ingestibles).ToHashSet();
                var armors = state.LoadOrder.PriorityOrder.Armor().WinningOverrides().ToDictionary(x => x.FormKey);
                var weapons = state.LoadOrder.PriorityOrder.Weapon().WinningOverrides().Select(x => x.FormKey).ToHashSet();

                foreach (var cobj in state.LoadOrder.PriorityOrder.ConstructibleObject().WinningOverrides())
                {
                    var workbenchKeywordFormKey = cobj.WorkbenchKeyword.FormKey;
                    if (!workbenchFilter.Contains(workbenchKeywordFormKey)) continue;

                    var items = cobj.Items;
                    if (items == null && !workbenchesThatNeedNoItems.Contains(workbenchKeywordFormKey)) continue;

                    formListsForWorkbench[workbenchKeywordFormKey](cobj);

                    if (items == null) continue;
                    foreach (var item in items)
                    {
                        var itemFormKey = item.Item.Item.FormKey;
                        var shouldUnburden = true;

                        if (doNotUnburdenFormKeys.Contains(itemFormKey))
                            shouldUnburden = false;
                        else if (ingredientsOrIngestibles.Contains(itemFormKey))
                        {
                            ingredientSet.Add(itemFormKey);
                            shouldUnburden = false;
                        }
                        else if (armors.TryGetValue(itemFormKey, out var theArmor))
                        {
                            if (theArmor.EditorID?.Contains("Ring Shank") == true)
                                shouldUnburden = true; // intermediate ingredient from Immersive Jewellery
                            else
                                shouldUnburden = false;
                        }
                        else if (weapons.Contains(itemFormKey))
                            shouldUnburden = false;

                        if (shouldUnburden)
                        {
                            allSet.Add(itemFormKey);
                            specificSet.Add(itemFormKey);
                        }
                    }
                }

            }
            else
            {
                foreach (var cobj in state.LoadOrder.PriorityOrder.ConstructibleObject().WinningOverrides())
                {
                    var workbenchKeywordFormKey = cobj.WorkbenchKeyword.FormKey;
                    if (!workbenchFilter.Contains(workbenchKeywordFormKey)) continue;

                    var items = cobj.Items;
                    if (items == null && !workbenchesThatNeedNoItems.Contains(workbenchKeywordFormKey)) continue;

                    formListsForWorkbench[workbenchKeywordFormKey](cobj);

                    if (items == null) continue;
                    foreach (var item in items)
                    {
                        var itemFormKey = item.Item.Item.FormKey;
                        var shouldUnburden = true;

                        if (doNotUnburdenFormKeys.Contains(itemFormKey))
                            shouldUnburden = false;
                        else if (state.LinkCache.TryResolve<IIngredientGetter>(itemFormKey, out _) || state.LinkCache.TryResolve<IIngestibleGetter>(itemFormKey, out _))
                        {
                            ingredientSet.Add(itemFormKey);
                            shouldUnburden = false;
                        }
                        else if (state.LinkCache.TryResolve<IArmorGetter>(itemFormKey, out var theArmor))
                        {
                            if (theArmor.EditorID?.Contains("Ring Shank") == true)
                                shouldUnburden = true; // intermediate ingredient from Immersive Jewellery
                            else
                                shouldUnburden = false;
                        }
                        else if (state.LinkCache.TryResolve<IWeaponGetter>(itemFormKey, out _))
                            shouldUnburden = false;

                        if (shouldUnburden)
                        {
                            allSet.Add(itemFormKey);
                            specificSet.Add(itemFormKey);
                        }
                    }
                }
            }

            Console.WriteLine("Adding found items to GeneralStores Form Lists...");

            var modCount = state.LoadOrder.PriorityOrder.Count();

            var modKeyToPriority = state.LoadOrder.PriorityOrder.Select((x, i) => (x.ModKey, i)).ToDictionary(x => x.ModKey, x => (uint)(modCount - x.i));

            IOrderedEnumerable<FormKey> orderByPriorityAndID(ISet<FormKey> formKeySet) => formKeySet.OrderBy(x => ((ulong)modKeyToPriority[x.ModKey] << 24) | x.ID);

            void applySetToFLST(FormKey flstKey, ISet<FormKey> set)
            {
                var flst = state.LinkCache.Resolve<IFormListGetter>(flstKey);
                if (flst is null) return;

                var flstEDID = flst.EditorID;

                var missingSet = new HashSet<FormKey>(set.Count);

                Console.WriteLine($"Found {set.Count} records that should be in {flstEDID}");

                var items = flst.Items.Select(x => x.FormKey).ToHashSet();

                foreach (var item in flst.Items.Select(x => x.FormKey))
                    if (!set.Remove(item))
                        missingSet.Add(item);

                if (missingSet.Count > 0)
                {
                    Console.WriteLine($"The following {missingSet.Count} records in {flstEDID} were not in the records we found:");
                    foreach (var item in orderByPriorityAndID(missingSet))
                        Console.WriteLine($"    {item}");
                }

                Console.WriteLine($"Found {set.Count} new records to add to {flstEDID}");

                if (set.Count == 0) return;

                var modifiedFlst = state.PatchMod.FormLists.GetOrAddAsOverride(flst);

                modifiedFlst.Items.AddRange(orderByPriorityAndID(set));
            }

            applySetToFLST(GeneralStores.FormList.xGSxAlchCookFLST, alchemyAndCookingSet);
            applySetToFLST(GeneralStores.FormList.xGSxAlchSmithFLST, alchemyAndSmithingSet);

            applySetToFLST(GeneralStores.FormList.xGSxFoodAllFLST, allFoodSet);
            applySetToFLST(GeneralStores.FormList.xGSxFoodCookedFLST, cookedFoodSet);
            applySetToFLST(GeneralStores.FormList.xGSxFoodRawFLST, rawFoodSet);

            applySetToFLST(GeneralStores.FormList.xGSxAllSmithFLST, allSmithingSet);
            applySetToFLST(GeneralStores.FormList.xGSxSmeltingFLST, smeltingSet);
            applySetToFLST(GeneralStores.FormList.xGSxSmithingFLST, smithingSet);
            applySetToFLST(GeneralStores.FormList.xGSxTanningFLST, tanningSet);

            // these FormLists have been merged into GeneralStores.esm in SkyrimSE/VR, but are in a separate plugin in SkyrimLE.
            switch (state.Settings.GameRelease)
            {
                case GameRelease.SkyrimSE:
                case GameRelease.SkyrimVR:
                    applySetToFLST(GeneralStores.FormList.xHFSxConstructionFLST, hearthFiresConstructionSet);
                    applySetToFLST(GeneralStores.FormList.xHFSxIngredientsFLST, hearthFiresIngredientSet);
                    break;
                case GameRelease.SkyrimLE:
                    if (state.LoadOrder.PriorityOrder.HasMod(HearthFireStores_GS.FormList.xHFSxConstructionFLST.ModKey, true))
                    {
                        applySetToFLST(HearthFireStores_GS.FormList.xHFSxConstructionFLST, hearthFiresConstructionSet);
                        applySetToFLST(HearthFireStores_GS.FormList.xHFSxIngredientsFLST, hearthFiresIngredientSet);
                    }
                    break;
                default:
                    throw new NotImplementedException("");
            }
        }
    }
}
