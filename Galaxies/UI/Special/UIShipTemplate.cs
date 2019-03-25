using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Progression;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    /// <summary>
    /// Showing the template player ship.
    /// </summary>
    class UIShipTemplate : UIGroup
    {

        PlayerShipTemplate playerShipTemplate;

        /// <summary>
        /// This button will act as the "Purchase" button if the ship hasn't been bought already, and as a "Use" button if it has been bought.
        /// </summary>
        UIButton button;

        public UIShipTemplate(Transform transform, PlayerShipTemplate playerShipTemplate, Screen screen) : base(transform, playerShipTemplate.Ship.Sprite, screen)
        {
            this.playerShipTemplate = playerShipTemplate;

            if (!playerShipTemplate.Unlocked)
            {
                button = AddUIElement(new UIButton(
                    new Transform(new Vector2(0, transform.Height / 2f + 50), new Vector2(transform.Width, 50)),
                    ContentHelper.Arial_Font,
                    "Buy [" + playerShipTemplate.Price + "]",
                    TextAlign.MiddleCenter,
                    5,
                    ContentHelper.Box4x4_Sprite,
                    new EventArg0(TryPurchasePlayerShipTemplate),
                    screen
                    ));
            }
            else
            {
                button = AddUIElement(new UIButton(
                    new Transform(new Vector2(0, transform.Height / 2f + 50), new Vector2(transform.Width, 50)),
                    ContentHelper.Arial_Font,
                    "Use",
                    TextAlign.MiddleCenter,
                    5,
                    ContentHelper.Box4x4_Sprite,
                    new EventArg1<PlayerShipTemplate>(ShipyardController.AssignPlayerShip, playerShipTemplate),
                    screen
                    ));
            }

            button.SetColor(new Color(28, 28, 28));

            //Create the stats text, displaying such things as Health and Shield etc.:
            AddUIElement(new UIText(
                new Transform(new Vector2(0, transform.Height / 2f + 225), new Vector2(400, 250)),
                ContentHelper.Arial_Font,
                string.Format("HP: {0}\nSHD: {1}\nDAM: {2}\nENR: {3}\nREG: {4}",
                                playerShipTemplate.Ship.BaseStats.Health,
                                playerShipTemplate.Ship.BaseStats.Shield,
                                playerShipTemplate.Ship.BaseStats.Damage,
                                playerShipTemplate.Ship.BaseStats.Energy,
                                playerShipTemplate.Ship.BaseStats.EnergyRegen),
                TextAlign.TopCenter,
                5,
                screen
            ));
        }

        private void TryPurchasePlayerShipTemplate()
        {
            if (!playerShipTemplate.Unlocked && PlayerController.Player.Balance.Extract(playerShipTemplate.Price))
            {
                //Assign the new ship.
                ShipyardController.AssignPlayerShip(playerShipTemplate);

                ///Repurpose the <see cref="button"/> to be used as the "Use" button.
                button.Text = "Use";
                button.SetOnClick(new EventArg1<PlayerShipTemplate>(ShipyardController.AssignPlayerShip, playerShipTemplate));
            }
        }

    }

}
