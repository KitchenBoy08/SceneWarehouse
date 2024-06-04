using MelonLoader;

using SceneWarehouse.Bonemenu;
using SceneWarehouse.Utilities;
using SLZ.Marrow.Warehouse;

using System;
using System.Linq;

namespace SceneWarehouse
{
    internal class Main : MelonMod
    {
        public override void OnInitializeMelon()
        {
            BonemenuManager.InitializeBoneMenu();
        }

        public override void OnLateInitializeMelon()
        {
            HookGenerator.InitHooks();
        }
    }
}
