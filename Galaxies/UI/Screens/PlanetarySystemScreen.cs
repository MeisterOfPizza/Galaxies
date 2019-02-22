using Galaxies.Controllers;
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

            var planetsColumn = AddUIElement(new UIScrollableColumn(column, GameUIController.TopLeftCorner(300, 600), 0, Color.White, new Vector2(300, 600), this, 5, 0, 200));

            foreach (Planet planet in PlanetarySystemController.CurrentPlanetarySystem.Planets)
            {
                if (!planet.Visited)
                {
                    planetsColumn.AddUIElement(new UIPlanet(column, Vector2.Zero, planet.Visit, this, planet));
                }
            }

            AddUIElement(new UIButton(
                SpriteHelper.Arial_Font,
                "Exit solar system",
                TextAlign.MiddleCenter,
                5,
                column,
                GameUIController.BottomRightCorner(300, 50),
                0,
                Color.White,
                new Vector2(300, 50),
                GameUIController.CreateGalaxyScreen,
                this
                ));
        }

    }

}
