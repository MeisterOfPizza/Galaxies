using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Special;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Galaxies.UI.Screens
{

    class CitadelScreen : Screen
    {

        UIShop uiShop;
        UIShipyard uiShipyard;

        public override void CreateUI(ContentManager content)
        {
            //Add background gif:
            AddUIElement(new UIBackgroundAnimation(
                new Transform(Alignment.MiddleCenter, new Vector2(GameUIController.WindowWidth, GameUIController.WindowHeight)),
                SpriteHelper.Citadel_Background_Animation,
                this
                ));

            //Add the shop:
            uiShop = AddUIElement(new UIShop(
                new Transform(Alignment.MiddleCenter, new Vector2(1300, 800)),
                this
                ));

            uiShop.Visable = false; //Hide the shop.

            //Add the shipyard:
            uiShipyard = AddUIElement(new UIShipyard(
                new Transform(Alignment.MiddleCenter, new Vector2(1200, 800)),
                this
                ));

            uiShipyard.Visable = false; //Hide the shipyard.

            //TODO: Add save button (and saves scollable column).

            //Go back button:
            AddUIElement(new UIButton(
                new Transform(Alignment.BottomLeft, new Vector2(300, 50)),
                SpriteHelper.Arial_Font,
                "Exit the Citadel",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(GameUIController.CreateGalaxyScreen),
                this
                ));

            //Open shop button:
            AddUIElement(new UIButton(
                new Transform(Transform.SetPosition(Alignment.BottomRight, new Vector2(-212.5f, 0), new Vector2(200, 50)), new Vector2(200, 50)),
                SpriteHelper.Arial_Font,
                "Shop",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(ToggleShop),
                this
                ));

            //Open shipyard button:
            AddUIElement(new UIButton(
                new Transform(Alignment.BottomRight, new Vector2(200, 50)),
                SpriteHelper.Arial_Font,
                "Shipyard",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(ToggleShipyard),
                this
                ));

            //Add the Top Info bar:
            AddUIElement(new UITopInfo(this));
        }

        private void ToggleShop()
        {
            uiShop.Visable = !uiShop.Visable; //Switch the visibility
            uiShipyard.Visable = false;
        }

        private void ToggleShipyard()
        {
            uiShipyard.Visable = !uiShipyard.Visable;  //Switch the visibility
            uiShop.Visable = false;
        }

    }

}
