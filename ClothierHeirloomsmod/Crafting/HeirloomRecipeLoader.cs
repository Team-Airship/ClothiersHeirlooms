using System;
using System.Collections.Generic;
using System.Text;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using Vintagestory.ServerMods;

namespace ClothierHeirloomsmod.Crafting
{
    public class HeirloomRecipeLoader: ModSystem
    {
        private ICoreServerAPI api;

        public override double ExecuteOrder()
        {
            return 1.0;
        }

        public override bool ShouldLoad(EnumAppSide side)
        {
            return side == EnumAppSide.Server;
        }

        bool classExclusiveRecipes = true;
        public override void AssetsLoaded(ICoreAPI api)
        {
            if (!(api is ICoreServerAPI serverApi)) return;

            classExclusiveRecipes = serverApi.World.Config.GetBool("classExclusiveRecipes", true);


            RecipeLoader.LoadRecipes<SpinnerRecipe>(serverApi, "spinner recipe", "recipes/spinner", classExclusiveRecipes, (r) => serverApi.RegisterSpinnerRecipe(r as SpinnerRecipe));
            RecipeLoader.LoadRecipes<AutoloomRecipe>(serverApi, "autoloom recipe", "recipes/autoloom", classExclusiveRecipes, (r) => serverApi.RegisterAutoloomRecipe(r as AutoloomRecipe));
            serverApi.World.Logger.StoryEvent(Lang.Get("The spinning of cloth..."));

        }
    }
}
