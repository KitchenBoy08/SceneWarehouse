using SceneWarehouse.Bonemenu;
using SLZ.Marrow.Warehouse;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneWarehouse.Utilities
{
    internal class HookGenerator
    {
        internal static void InitHooks()
        {
            BoneLib.Hooking.OnLevelInitialized += RecentLevelsMenu.AddRecent;

            AssetWarehouse.OnReady(new Action(() =>
            {
                LevelFinderMenu.Crates = AssetWarehouse.Instance.GetCrates().ToArray().ToList();
                LevelFinderMenu.Reset();

                Action action = () =>
                {
                    LevelFinderMenu.Crates = AssetWarehouse.Instance.GetCrates().ToArray().ToList();
                    LevelFinderMenu.Reset();
                };

                Il2CppSystem.Action il2Action = (Il2CppSystem.Action)action;

                AssetWarehouse.Instance.OnChanged += il2Action;
            }));
        }
    }
}
