using Galaxies.Controllers;
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

            var planetsColumn = AddUIElement(new UIScrollableColumn(column, GameUIController.TopLeftCorner(300, 600), 0, Color.White, this, 5, 0, 200));
            planetsColumn.SetDrawSize(300, 600);

            foreach (Planet planet in PlanetarySystemController.CurrentPlanetarySystem.Planets)
            {
                planetsColumn.AddUIElement(new UIPlanet(column, Vector2.Zero, planet.Visit, this, planet));
            }
        }

    }

}
