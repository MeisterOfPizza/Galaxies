using Galaxies.Controllers;
using Galaxies.Datas.Space;
using Galaxies.UI;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Space.Events
{

    class ItemDropPlanetEvent : PlanetEvent
    {

        public ItemDropPlanetEventData data;

        public override PlanetEventData Data
        {
            get
            {
                return data;
            }
        }

        private UIMessageBox messageBox;

        public ItemDropPlanetEvent(ItemDropPlanetEventData data)
        {
            this.data = data;
        }

        public override void Trigger()
        {
            messageBox = GameUIController.CurrentScreen.AddUIElement(new UIMessageBox(
                MainGame.Singleton.Content.Load<SpriteFont>("Fonts/Arial"),
                "Item drops 1 2 3 4 5",
                TextAlign.MiddleCenter,
                5,
                MainGame.Singleton.Content.Load<Texture2D>("Sprites/UI/Column"),
                GameUIController.Center(),
                0,
                Color.White,
                new Vector2(500, 150),
                _Trigger,
                GameUIController.CurrentScreen
                ));
        }

        /// <summary>
        /// Give the player the items.
        /// </summary>
        private void _Trigger()
        {
            messageBox.Screen.RemoveUIElement(messageBox);
            messageBox.Screen.RemoveUIElement(messageBox.OkBtn);
            PlanetEventController.TriggerNextEvent();
        }

    }

}
