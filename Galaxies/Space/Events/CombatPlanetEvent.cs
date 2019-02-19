using Galaxies.Controllers;
using Galaxies.Datas.Space;
using Galaxies.UI;
using Galaxies.UI.Elements;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Space.Events
{

    class CombatPlanetEvent : PlanetEvent
    {

        public CombatPlanetEventData data;

        public override PlanetEventData Data
        {
            get
            {
                return data;
            }
        }

        private UIMessageBox messageBox;

        public CombatPlanetEvent(CombatPlanetEventData data)
        {
            this.data = data;
        }

        public override void Trigger()
        {
            messageBox = GameUIController.CurrentScreen.AddUIElement(new UIMessageBox(
                MainGame.Singleton.Content.Load<SpriteFont>("Fonts/Arial"),
                "Meeting enemy XYZ",
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
        /// Enter combat.
        /// </summary>
        private void _Trigger()
        {
            messageBox.Screen.RemoveUIElement(messageBox); //HACK: Really needed? If we're discarding the current screen, do we really need to remove the UI Element first?
            messageBox.Screen.RemoveUIElement(messageBox.OkBtn);
            CombatController.StartBattle(this);
            GameUIController.CreateCombatScreen();
        }

    }

}
