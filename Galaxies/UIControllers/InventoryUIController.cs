using Galaxies.Core;
using Galaxies.UI.Screens;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Special
{

    static class InventoryUIController
    {

        public static UIInventory UIInventory { get; private set; }

        public static void CreateWindow(Screen screen)
        {
            if (UIInventory != null)
            {
                RemoveWindow();
            }
            else
            {
                UIInventory = screen.AddUIElement(new UIInventory(new Transform(Alignment.MiddleCenter, new Vector2(GameUIController.WindowWidth, GameUIController.WindowHeight)), screen));
            }
        }

        public static void RemoveWindow()
        {
            if (UIInventory != null)
            {
                UIInventory.Close();
                UIInventory = null;
            }
        }

    }

}
