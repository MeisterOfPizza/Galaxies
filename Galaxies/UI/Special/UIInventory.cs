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

        public UIScrollableGrid ItemGrid { get; private set; }

        public UIInventory(Transform transform, Screen screen) : base(transform, null, screen)
        {
            AddUIElement(new UIText(
                new Transform(Transform.SetPosition(Alignment.MiddleCenter, new Vector2(0, -transform.Height / 2f - 50), new Vector2(transform.Width, 50)), new Vector2(transform.Width, 50)),
                SpriteHelper.Arial_Font,
                "Inventory",
                TextAlign.MiddleLeft,
                5,
                screen
                ));

            ItemGrid = AddUIElement(new UIScrollableGrid(
                new Transform(Alignment.MiddleCenter, new Vector2(transform.Width, transform.Height)),
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                screen,
                new Vector4(10),
                new Vector2(5),
                new Vector2(400, 300)
                ));

            foreach (Item item in PlayerShip.Singleton.Inventory.Items)
            {
                var itemPointer = item; //Create a new pointer (reference).

                ItemGrid.AddUIElement(
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
                    new Transform(Transform.SetPosition(Alignment.MiddleCenter, new Vector2(0, transform.Height / 2f + 50), new Vector2(100, 50)), new Vector2(100, 50)),
                    SpriteHelper.Arial_Font,
                    "Close",
                    TextAlign.MiddleCenter,
                    5,
                    SpriteHelper.GetSprite("Sprites/UI/Column"),
                    new EventArg0(Close),
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
