using Harmony;
using SMLHelper.V2.Crafting;

namespace SNHardcorePlus.Patches
{
    [HarmonyPatch(typeof(CraftData))]
    [HarmonyPatch("Get")]
    class CraftData_Get_Patch
    {
        public static void Postfix(ref ITechData __result)
        {
            var patchedTech = new TechData() { craftAmount = __result.craftAmount };

            for (int i = 0; i < __result.ingredientCount; i++)
            {
                IIngredient ingredient = __result.GetIngredient(i);
                Ingredient patchedIngredient = new Ingredient(ingredient.techType, ingredient.amount*HCPSettings.Instance.CraftingCostMultiplier);
                patchedTech.Ingredients.Add(patchedIngredient);
            }

            for (int i = 0; i < __result.ingredientCount; i++)
            {
                patchedTech.LinkedItems.Add(__result.GetLinkedItem(i));
            }

            __result = patchedTech;
        }
    }
}
