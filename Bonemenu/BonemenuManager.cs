using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;

using UnityEngine;

using SceneWarehouse.Utilities;

using SLZ.Marrow.SceneStreaming;
using SLZ.Marrow.Warehouse;

namespace SceneWarehouse.Bonemenu
{
    internal class BonemenuManager
    {
        internal static MenuCategory MainCategory;

        internal static void InitializeBoneMenu()
        {
            MainCategory = MenuManager.CreateCategory("<color=#FFFFFF>Scene</color> <color=#0CB800>W</color><color=#0ABA1C>a</color><color=#09BC38>r</color><color=#07BE55>e</color><color=#06C071>h</color><color=#05C38D>o</color><color=#04C5AA>u</color><color=#02C7C6>s</color><color=#01C9E2>e</color>", Color.white);

            LevelFinderMenu.CreateLevelFinder(MainCategory);
            FavoriteLevelsMenu.CreateFavoriteMenu(MainCategory);
            RecentLevelsMenu.CreateRecentsCategory(MainCategory);
        }

        internal static MenuCategory CreateLevelCategory(MenuCategory category, Crate levelCrate)
        {
            if (levelCrate.Barcode == null || levelCrate.Barcode.ID == null || levelCrate.Title == null)
                return null;

            var levelCategory = category.CreateCategory(levelCrate.Title, Color.white);

            levelCategory.CreateFunctionElement("Load Level", Color.green, () =>
            {
                SceneStreamer.Load(levelCrate.Barcode.ID);
            });

            FunctionElement favoriteElement = null;

            if (FavoriteManager.IsFavorite(levelCrate.Barcode.ID, levelCrate.Title))
                favoriteElement = levelCategory.CreateFunctionElement("Unfavorite Level", Color.red, () => OnClickFavorite(favoriteElement, levelCrate));
            else
                favoriteElement = levelCategory.CreateFunctionElement("Favorite Level", Color.yellow, () => OnClickFavorite(favoriteElement, levelCrate));

            return levelCategory;
        }

        private static void OnClickFavorite(FunctionElement functionElement, Crate levelCrate)
        {
            if (levelCrate == null)
                return;

            if (FavoriteManager.IsFavorite(levelCrate.Barcode.ID, levelCrate.Title))
            {
                FavoriteManager.RemoveFavorite(levelCrate.Barcode.ID, levelCrate.Title);
                functionElement.SetName("Favorite Level");
                functionElement.SetColor(Color.yellow);
            }
            else
            {
                FavoriteManager.AddFavorite(levelCrate.Barcode.ID, levelCrate.Title);
                functionElement.SetName("Unfavorite Level");
                functionElement.SetColor(Color.red);
            }
        }
    }
}
