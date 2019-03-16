using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Special;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace Galaxies.UI.Screens
{

    class PlanetarySystemScreen : Screen
    {

        public async override void CreateUIAsync()
        {
            AddUIElement(new UIBackgroundAnimation(
                new Transform(Alignment.MiddleCenter, GameUIController.Window.ClientBounds.Size.ToVector2()),
                SpriteHelper.Space_Background_Animation_1,
                this
                ));

            var column = SpriteHelper.GetSprite("Sprites/UI/Column");

            var planetsColumn = AddUIElement(new UIScrollableColumn(new Transform(Alignment.TopLeft, new Vector2(300, 600)), column, this, new Vector4(5, 0, 5, 0), Vector2.Zero, 200));

            foreach (Planet planet in PlanetarySystemController.CurrentPlanetarySystem.Planets)
            {
                if (!planet.Visited)
                {
                    planetsColumn.AddUIElement(new UIPlanet(new Transform(Vector2.Zero), column, new EventArg0(planet.Visit), this, planet));
                }
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

            //Add the Top Info bar:
            AddUIElement(new UITopInfo(this));
        }

    }

}
