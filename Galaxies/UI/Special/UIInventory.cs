using Galaxies.Core;
using Galaxies.Entities;
using Galaxies.Extensions;
using Galaxies.Items;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Special
{

    class UIInventory : UIGroup
    {

        UIScrollableGrid itemGrid;

        public UIInventory(Transform transform, Screen screen) : base(transform, null, screen)
        {
            AddUIElement(new UIText(
                new Transform(Transform.SetPosition(Alignment.MiddleCenter, new Vector2(0, -350), new Vector2(1200, 50)), new Vector2(1200, 50)),
                SpriteHelper.Arial_Font,
                "Inventory",
                TextAlign.MiddleLeft,
                5,
                screen
                ));

            itemGrid = AddUIElement(new UIScrollableGrid(
                new Transform(Alignment.MiddleCenter, new Vector2(1200, 600)),
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                screen,
                new Vector4(10),
                new Vector2(5),
                new Vector2(400, 300)
                ));

            foreach (Item item in PlayerShip.Singleton.Inventory.Items)
            {
                var itemPointer = item; //Create a new pointer (reference).

                var v = itemGrid.AddUIElement(
                    new UIItem(
                        new Transform(new Vector2(400, 300)),
                        SpriteHelper.GetSprite("Sprites/UI/Column"),
                        screen,
                        itemPointer
                        )
                );
            }

            AddUIElement(
                new UIButton(
                    new Transform(Transform.SetPosition(Alignment.MiddleCenter, new Vector2(0, 350), new Vector2(100, 50)), new Vector2(100, 50)),
                    SpriteHelper.Arial_Font,
                    "Close",
                    TextAlign.MiddleCenter,
                    5,
                    SpriteHelper.GetSprite("Sprites/UI/Column"),
                    new EventArg0(InventoryUIController.RemoveWindow),
                    screen
                    )
                );
        }

        public void Close()
        {
            Screen.RemoveUIElement(this);
        }

    }

}
