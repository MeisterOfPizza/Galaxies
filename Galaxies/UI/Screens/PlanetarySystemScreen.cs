using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
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

            /*
            var planetsColumn = AddUIElement(new UIScrollableColumn(column, GameUIController.TopLeftCorner(300, 600), 0, Color.White, new Vector2(300, 600), this, new Vector4(5, 0, 5, 0), Vector2.Zero, 200));

            foreach (Planet planet in PlanetarySystemController.CurrentPlanetarySystem.Planets)
            {
                if (!planet.Visited)
                {
                    planetsColumn.AddUIElement(new UIPlanet(column, Vector2.Zero, new EventArg0(planet.Visit), this, planet));
                }
            }
            */
            
            var testGrid = AddUIElement(new UIScrollableGrid(
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

            for (int i = 0; i < 14; i++)
            {
                testGrid.AddUIElement(new UIButton(
                    new Transform(new Vector2(200, 100)),
                    SpriteHelper.Arial_Font,
                    (i + 1).ToString(),
                    TextAlign.MiddleCenter,
                    0,
                    column,
                    null,
                    this
                    ));
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

    }

}
