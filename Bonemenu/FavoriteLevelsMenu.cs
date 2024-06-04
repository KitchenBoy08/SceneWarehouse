using UnityEngine;

using BoneLib.BoneMenu.Elements;
using BoneLib.BoneMenu;

using SLZ.Marrow.SceneStreaming;

using SceneWarehouse.Utilities;

namespace SceneWarehouse.Bonemenu
{
    internal class FavoriteLevelsMenu
    {
        internal static MenuCategory FavoriteCategory;

        internal static void CreateFavoriteMenu(MenuCategory menuCategory)
        {
            FavoriteManager.FavoriteLevels = FavoriteManager.LoadFavoriteLevels();

            FavoriteCategory = menuCategory.CreateCategory("Favorite Levels", Color.yellow);
            RefreshFavoriteList();
        }

        internal static void RefreshFavoriteList()
        {
            using (BatchedBoneMenu.Create())
            {
                FavoriteCategory.Elements.Clear();

                foreach (var pair in FavoriteManager.FavoriteLevels)
                {
                    CreateFavoriteElement(FavoriteCategory, pair.Key, pair.Value);
                }
            }
        }

        private static MenuCategory CreateFavoriteElement(MenuCategory category, string barcode, string levelTitle)
        {
            var favoriteCategory = category.CreateCategory(levelTitle, Color.white);

            favoriteCategory.CreateFunctionElement("Load Level", Color.green, () =>
            {
                SceneStreamer.Load(barcode);
            });
            favoriteCategory.CreateFunctionElement("Unfavorite", Color.red, () =>
            {
                Utilities.FavoriteManager.RemoveFavorite(barcode, levelTitle);
                RefreshFavoriteList();
                MenuManager.SelectCategory(FavoriteCategory);
            });

            return favoriteCategory;
        }
    }
}
