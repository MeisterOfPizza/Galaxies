using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Entities;
using Galaxies.Extensions;
using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Special;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Screens
{

    class GalaxyScreen : Screen
    {

        private UIInventory inventory;

        public override void CreateUI(ContentManager content)
        {
            var columnSprite           = content.Load<Texture2D>("Sprites/UI/Column");
            /*
            var backToMenuSprite       = content.Load<Texture2D>("Sprites/UI/BackToMenu");
            var galaxyBackgroundSprite = content.Load<Texture2D>("Sprites/UI/Galaxy Background");
            var statsSprite            = content.Load<Texture2D>("Sprites/UI/Stats");
            */

            //var visitablesColumn = AddUIElement(new UIScrollableColumn(columnSprite, GameUIController.TopLeftCorner(300, 600), 0, Color.White, new Vector2(300, 600), this, new Vector4(5, 0, 5, 0), new Vector2(0, 5), 200));
            var visitablesColumn = AddUIElement(new UIScrollableColumn(new Transform(Alignment.MiddleCenter, new Vector2(300, 600)), columnSprite, this, new Vector4(5, 0, 5, 0), new Vector2(0, 5), 200));

            AddUIElement(new UIButton(new Transform(Alignment.BottomLeft, new Vector2(100, 100)), SpriteHelper.Arial_Font, "Menu", TextAlign.MiddleCenter, 5, columnSprite, new EventArg0(GameUIController.CreateMenuScreen), this));
            
            //Creating visitables
            foreach (IVisitable visitable in GalaxyController.Visitables)
            {
                ///<see cref="PlanetarySystem.Visit"/> and <see cref="Citadel.Visit"/>
                for (int i = 0; i < 10; i++)
                {
                    visitablesColumn.AddUIElement(new UIPlanetarySystem(new Transform(Vector2.Zero), columnSprite, new EventArg0(visitable.Visit), this, visitable));
                }
            }

            AddUIElement(new UIButton(
                new Transform(Alignment.BottomRight, new Vector2(250, 50)),
                SpriteHelper.Arial_Font,
                "Inventory",
                TextAlign.MiddleCenter,
                5,
                columnSprite,
                new EventArg0(CreateInventory),
                this
                ));

            /*
            AddUIElement(new UIElement(backToMenuSprite, GameUIController.BottomLeftCorner(backToMenuSprite.Width, backToMenuSprite.Height), 0, Color.White, null)); //TODO: Back to menu
            AddUIElement(new UIElement(galaxyBackgroundSprite, Vector2.Zero, 0, Color.White, null));
            AddUIElement(new ListBox(statsSprite, GameUIController.BottomRightCorner(statsSprite.Width, statsSprite.Height), 0, Color.White, null));
            */
        }

        private void CreateInventory()
        {
            if (inventory != null && UIElements.Contains(inventory)) //Check if it hasn't already been removed.
            {
                RemoveUIElement(inventory);
            }
            else
            {
                inventory = AddUIElement(new UIInventory(new Transform(Alignment.MiddleCenter, new Vector2(1200, 600)), this, PlayerShip.Singleton.Inventory.Items));
            }
        }

    }

}
