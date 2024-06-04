using BoneLib;
using BoneLib.BoneMenu.Elements;

using UnityEngine;

namespace SceneWarehouse.Bonemenu
{
    internal class RecentLevelsMenu
    {
        internal static MenuCategory RecentsCategory;

        internal static void CreateRecentsCategory(MenuCategory menuCategory)
        {
            RecentsCategory = menuCategory.CreateCategory("Recent Levels", Color.magenta);
        }

        internal static void AddRecent(LevelInfo levelInfo)
        {
            var levelCategory = BonemenuManager.CreateLevelCategory(RecentsCategory, levelInfo.levelReference.Crate);

            int index = RecentsCategory.Elements.IndexOf(levelCategory);
            RecentsCategory.Elements.RemoveAt(index);
            RecentsCategory.Elements.Insert(0, levelCategory);
        }
    }
}
