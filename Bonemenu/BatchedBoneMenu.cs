using BoneLib.BoneMenu.Elements;
using BoneLib.BoneMenu;
using BoneLib;

using System;

namespace SceneWarehouse.Bonemenu
{
    // Thanks Lakatrazz, full credit for this file goes to him
    public sealed class BatchedBoneMenu : IDisposable
    {
        private Action<MenuCategory, MenuElement> _action;

        private BatchedBoneMenu(Action<MenuCategory, MenuElement> action)
        {
            _action = action;
        }

        public static BatchedBoneMenu Create()
        {
            var instance = new BatchedBoneMenu(MenuCategory.OnElementCreated);
            MenuCategory.OnElementCreated = null;
            return instance;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            MenuCategory.OnElementCreated = _action;

            if (_action != null)
            {
                SafeActions.InvokeActionSafe(_action, MenuManager.ActiveCategory.Parent, MenuManager.ActiveCategory);
                _action = null;
            }
        }
    }
}
