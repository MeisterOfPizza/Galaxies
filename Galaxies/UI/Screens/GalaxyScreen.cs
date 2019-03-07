﻿using Galaxies.Controllers;
using Galaxies.Core;
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
            AddUIElement(new UIBackgroundAnimation(
                new Transform(Alignment.MiddleCenter, GameUIController.Window.ClientBounds.Size.ToVector2()),
                SpriteHelper.Citadel_Background_Animation,
                this
                ));

            var columnSprite = content.Load<Texture2D>("Sprites/UI/Column");

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

            //Add the Top Info bar:
            AddUIElement(new UITopInfo(this));
        }

        private void CreateInventory()
        {
            if (inventory != null && UIElements.Contains(inventory)) //Check if it hasn't already been removed.
            {
                RemoveUIElement(inventory);
            }
            else
            {
                inventory = AddUIElement(new UIInventory(new Transform(Alignment.MiddleCenter, new Vector2(1200, 600)), this, PlayerController.Player.Inventory.Items, "Inventory", true));
            }
        }

    }

}
