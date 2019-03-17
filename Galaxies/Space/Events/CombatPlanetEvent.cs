using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Datas;
using Galaxies.Datas.Enemies;
using Galaxies.Datas.Helpers;
using Galaxies.Datas.Space;
using Galaxies.Extensions;
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

        /// <summary>
        /// Randomly selected enemy.
        /// </summary>
        public EnemyShipData EnemyShipData { get; private set; }

        public CombatPlanetEvent(CombatPlanetEventData data)
        {
            this.data = data;
        }

        public override void Trigger()
        {
            var dataPointer = data.EnemyPointers[Random.Next(data.EnemyPointers.Length)];
            EnemyShipData = DataController.LoadData<EnemyShipData>(dataPointer.Id, DataFileType.Enemies);

            messageBox = GameUIController.CurrentScreen.AddUIElement(new UIMessageBox(
                new Transform(Alignment.MiddleCenter, new Vector2(500, 150)),
                MainGame.Singleton.Content.Load<SpriteFont>("Fonts/Arial"),
                "Detected a hostile ship! [" + EnemyShipData.Name + "]",
                TextAlign.TopCenter,
                5,
                MainGame.Singleton.Content.Load<Texture2D>("Sprites/UI/Column"),
                new EventArg0(_Trigger),
                GameUIController.CurrentScreen
                ));
        }

        /// <summary>
        /// Enter combat.
        /// </summary>
        private void _Trigger()
        {
            messageBox.Screen.RemoveUIElement(messageBox); //HACK: Really needed? If we're discarding the current screen, do we really need to remove the UI Element first?
            CombatController.StartBattle(this);
            GameUIController.CreateCombatScreen();
        }

    }

}
