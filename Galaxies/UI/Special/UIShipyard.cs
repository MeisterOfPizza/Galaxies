using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Special
{

    /// <summary>
    /// Used by the player to switch between ships.
    /// </summary>
    class UIShipyard : UIGroup
    {

        public UIShipyard(Transform transform, Screen screen) : base(transform, null, screen)
        {
            //This will NOT work if any of the first three player ship templates gets removed!

            AddUIElement(new UIShipTemplate(
                new Transform(new Vector2(-400, 0), new Vector2(200)),
                ShipyardController.PlayerShipTemplates[0],
                screen
                ));

            AddUIElement(new UIShipTemplate(
                new Transform(new Vector2(200)),
                ShipyardController.PlayerShipTemplates[1],
                screen
                ));

            AddUIElement(new UIShipTemplate(
                new Transform(new Vector2(400, 0), new Vector2(200)),
                ShipyardController.PlayerShipTemplates[2],
                screen
                ));
        }

    }

}
