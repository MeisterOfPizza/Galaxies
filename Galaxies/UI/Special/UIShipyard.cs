using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Entities;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    /// <summary>
    /// Used by the player to switch between ships.
    /// </summary>
    class UIShipyard : UIGroup
    {

        public UIShipyard(Transform transform, Screen screen) : base(transform, null, screen)
        {
            AddUIElement(new UIShipTemplate(
                new Transform(new Vector2(-400, 0), new Vector2(200)),
                ShipyardController.PlayerShip_Template_Fighter,
                screen
                ));

            AddUIElement(new UIShipTemplate(
                new Transform(new Vector2(200)),
                ShipyardController.PlayerShip_Template_Bomber,
                screen
                ));

            AddUIElement(new UIShipTemplate(
                new Transform(new Vector2(400, 0), new Vector2(200)),
                ShipyardController.PlayerShip_Template_Destroyer,
                screen
                ));
        }

    }

    /// <summary>
    /// Showing the template player ship.
    /// </summary>
    class UIShipTemplate : UIGroup
    {

        PlayerShip playerShipTemplate;

        public UIShipTemplate(Transform transform, PlayerShip playerShipTemplate, Screen screen) : base(transform, playerShipTemplate.Sprite, screen)
        {
            this.playerShipTemplate = playerShipTemplate;
        }

    }

}
