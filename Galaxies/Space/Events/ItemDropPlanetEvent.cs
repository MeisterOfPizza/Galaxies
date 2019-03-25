using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Datas.Items;
using Galaxies.Datas.Space;
using Galaxies.Extensions;
using Galaxies.Items;
using Galaxies.UI;
using Galaxies.UI.Elements;
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

        private Item[] items;

        public ItemDropPlanetEvent(ItemDropPlanetEventData data)
        {
            this.data = data;
        }

        public override void Trigger()
        {
            var dataObjs = data.GetDataFromIds();
            items = new Item[dataObjs.Length];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = ((ItemData)dataObjs[i]).CreateItem(PlayerController.Player.Inventory);
            }

            string itemNames = items.Length > 0 ? "You found " : "No items found";

            for (int i = 0; i < items.Length; i++)
            {
                itemNames += items[i].Data.Name;

                if (i < items.Length - 1)
                {
                    itemNames += ", ";
                }
            }

            messageBox = GameUIController.CurrentScreen.AddUIElement(new UIMessageBox(
                new Transform(Alignment.MiddleCenter, new Vector2(500, 250)),
                MainGame.Singleton.Content.Load<SpriteFont>("Fonts/Arial"),
                itemNames,
                TextAlign.TopCenter,
                5,
                ContentHelper.Box4x4_Sprite,
                new EventArg0(_Trigger),
                GameUIController.CurrentScreen
                ));
        }

        /// <summary>
        /// Give the player the items.
        /// </summary>
        private void _Trigger()
        {
            PlayerController.Player.Inventory.AddItems(items);

            messageBox.Screen.RemoveUIElement(messageBox);
            PlanetEventController.TriggerNextEvent();
        }

    }

}
