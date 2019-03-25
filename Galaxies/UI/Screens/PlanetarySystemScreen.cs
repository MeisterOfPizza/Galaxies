using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Special;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Screens
{

    class PlanetarySystemScreen : Screen
    {

        public override void CreateUI()
        {
            AddUIElement(new UIBackgroundAnimation(
                new Transform(Alignment.MiddleCenter, GameUIController.WindowSize),
                ContentHelper.Space_Background_Animation_1,
                this
                ));

            //Scrollable column with all the planets:
            var planetsColumn = AddUIElement(new UIScrollableColumn(
                new Transform(Alignment.MiddleCenter, new Vector2(300, 600)),
                ContentHelper.Box4x4_Sprite,
                this,
                new Vector4(5, 0, 5, 0),
                Vector2.Zero,
                200
                ));

            planetsColumn.SetColor(new Color(56, 56, 56));

            //Creating the planet UIs:
            foreach (Planet planet in PlanetarySystemController.CurrentPlanetarySystem.Planets)
            {
                if (!planet.Visited)
                {
                    planetsColumn.AddUIElement(new UIPlanet(
                        new Transform(Vector2.Zero),
                        ContentHelper.Box4x4_Sprite,
                        new EventArg0(planet.Visit),
                        this,
                        planet
                        ));
                }
            }

            AddUIElement(new UIButton(
                new Transform(Alignment.BottomRight, new Vector2(300, 50)),
                ContentHelper.Arial_Font,
                "Exit solar system",
                TextAlign.MiddleCenter,
                5,
                ContentHelper.Box4x4_Sprite,
                new EventArg1<EventArg>(GameUIController.CreateGalaxyScreen, null),
                this
                )).SetColor(new Color(28, 28, 28));

            //Add the Top Info bar:
            AddUIElement(new UITopInfo(this));
        }

    }

}
