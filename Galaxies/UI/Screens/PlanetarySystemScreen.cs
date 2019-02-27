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

    class PlanetarySystemScreen : Screen
    {

        public override void CreateUI(ContentManager content)
        {
            var column = content.Load<Texture2D>("Sprites/UI/Column");

            var planetsColumn = AddUIElement(new UIScrollableColumn(new Transform(Alignment.TopLeft, new Vector2(300, 600)), column, this, new Vector4(5, 0, 5, 0), Vector2.Zero, 200));

            foreach (Planet planet in PlanetarySystemController.CurrentPlanetarySystem.Planets)
            {
                if (!planet.Visited)
                {
                    planetsColumn.AddUIElement(new UIPlanet(new Transform(Vector2.Zero), column, new EventArg0(planet.Visit), this, planet));
                }
            }
            
            testGrid = AddUIElement(new UIScrollableGrid(
                new Transform(Alignment.MiddleCenter, new Vector2(600, 300)),
                column,
                this,
                new Vector4(5),
                new Vector2(5),
                new Vector2(200, 100)
                ));

            /*
            testColumn = AddUIElement(new UIScrollableColumn(
                column,
                GameUIController.Center(),
                0,
                Color.White,
                new Vector2(200, 300),
                this,
                new Vector4(5),
                new Vector2(5),
                100
                ));
                */

            for (int i = 0; i < 100; i++)
            {
                var btn = testGrid.AddUIElement(new UIButton(
                    new Transform(new Vector2(200, 100)),
                    SpriteHelper.Arial_Font,
                    (i + 1).ToString(),
                    TextAlign.MiddleCenter,
                    0,
                    column,
                    null,
                    this
                    ));

                btn.SetOnClick(new EventArg1<UIElement>(Test, testGrid));
            }

            AddUIElement(new UIButton(
                new Transform(Alignment.BottomRight, new Vector2(300, 50)),
                SpriteHelper.Arial_Font,
                "Exit solar system",
                TextAlign.MiddleCenter,
                5,
                column,
                new EventArg0(GameUIController.CreateGalaxyScreen),
                this
                ));
        }

        UIScrollableGrid testGrid;

        void Test(UIElement element)
        {
            testGrid.RemoveUIElement(element);
        }

    }

}
