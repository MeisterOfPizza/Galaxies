using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Entities;
using Galaxies.Extensions;
using Galaxies.Progression;
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
                    SpriteHelper.Arial_Font,
                    "Buy [" + playerShipTemplate.Price + "]",
                    TextAlign.MiddleCenter,
                    5,
                    SpriteHelper.GetSprite("Sprites/UI/Column"),
                    new EventArg0(TryPurchasePlayerShipTemplate),
                    screen
                    ));
            }
            else
            {
                button = AddUIElement(new UIButton(
                    new Transform(new Vector2(0, transform.Height / 2f + 50), new Vector2(transform.Width, 50)),
                    SpriteHelper.Arial_Font,
                    "Use",
                    TextAlign.MiddleCenter,
                    5,
                    SpriteHelper.GetSprite("Sprites/UI/Column"),
                    new EventArg1<PlayerShipTemplate>(ShipyardController.AssignPlayerShip, playerShipTemplate),
                    screen
                    ));
            }

            //Create the stats text, displaying such things as Health and Shield etc.:
            AddUIElement(new UIText(
                new Transform(new Vector2(0, transform.Height / 2f + 225), new Vector2(400, 250)),
                SpriteHelper.Arial_Font,
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }

}
