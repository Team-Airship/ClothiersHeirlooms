using System;
using System.Collections.Generic;
using System.Text;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace ClothierHeirloomsmod.Crafting
{
    public static class ApiAdditions
    {
        public static List<SpinnerRecipe> GetSpinnerRecipes(this ICoreAPI api)
        {
            return api.ModLoader.GetModSystem<HeirloomRecipeRegistrySysem>().SpinnerRecipes;
        }
        public static List<AutoloomRecipe> GetAutoloomRecipes(this ICoreAPI api)
        {
            return api.ModLoader.GetModSystem<HeirloomRecipeRegistrySysem>().AutoloomRecipes;
        }

        /// <summary>
        /// Registers a Spinner recipe. this was hard coded, but I want to make it proper
        /// </summary>
        /// <param name="api"></param>
        /// <param name="r"></param>
        public static void RegisterSpinnerRecipe(this ICoreServerAPI api, SpinnerRecipe r)
        {
            api.ModLoader.GetModSystem<HeirloomRecipeRegistrySysem>().RegisterSpinnerRecipe(r);
        }

        /// <summary>
        /// Registers a Autoloom recipe. this was hard coded, but I want to make it proper
        /// </summary>
        /// <param name="api"></param>
        /// <param name="r"></param>
        public static void RegisterAutoloomRecipe(this ICoreServerAPI api, AutoloomRecipe r)
        {
            api.ModLoader.GetModSystem<HeirloomRecipeRegistrySysem>().RegisterAutoloomRecipe(r);
        }
    }

    public class DisableHeirloomRecipeRegisteringSystem : ModSystem
    {
        public override double ExecuteOrder() => 99999;
        public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Server;
        public override void AssetsFinalize(ICoreAPI api)
        {
            HeirloomRecipeRegistrySysem.canRegister = false;
        }
    }

    public class HeirloomRecipeRegistrySysem : ModSystem
    {
        public static bool canRegister = true;

        /// <summary>
        /// List of all loaded Spinner recipes
        /// </summary>
        public List<SpinnerRecipe> SpinnerRecipes = new List<SpinnerRecipe>();

        /// <summary>
        /// List of all loaded Autoloom recipes
        /// </summary>
        public List<AutoloomRecipe> AutoloomRecipes = new List<AutoloomRecipe>();


        ICoreAPI api;
        public override double ExecuteOrder()
        {
            return 0.6;
        }

        public override void StartPre(ICoreAPI api)
        {
            this.api = api;
            canRegister = true;
        }

        public override void Start(ICoreAPI api)
        {
            SpinnerRecipes = api.RegisterRecipeRegistry<RecipeRegistryGeneric<SpinnerRecipe>>("spinnerrecipes").Recipes;
            AutoloomRecipes = api.RegisterRecipeRegistry<RecipeRegistryGeneric<AutoloomRecipe>>("autoloomrecipes").Recipes;
        }



        public void RegisterSpinnerRecipe(SpinnerRecipe recipe)
        {
            if (!canRegister) throw new InvalidOperationException("Coding error: Can no long register cooking recipes. Register them during AssetsLoad/AssetsFinalize and with ExecuteOrder < 99999");

            SpinnerRecipes.Add(recipe);
        }
        public void RegisterAutoloomRecipe(AutoloomRecipe recipe)
        {
            if (!canRegister) throw new InvalidOperationException("Coding error: Can no long register cooking recipes. Register them during AssetsLoad/AssetsFinalize and with ExecuteOrder < 99999");

            AutoloomRecipes.Add(recipe);
        }

    }
}
