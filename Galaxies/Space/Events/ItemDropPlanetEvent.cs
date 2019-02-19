using Galaxies.Controllers;
using Galaxies.Datas.Items;
using Galaxies.Datas.Space;
using Galaxies.Entities;
using Galaxies.Items;
using Galaxies.UI;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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

        private List<Item> items;

        public ItemDropPlanetEvent(ItemDropPlanetEventData data)
        {
            this.data = data;
        }

        public override void Trigger()
        {
            items = new List<Item>();
            foreach (string id in data.ItemIds)
            {
                //loadedItems.Add(new Item(DataController.LoadData<ItemData>(id, DataFileType.Items), PlayerShip.Singleton.Inventory)); //TODO: Fix so that we can load items of all sorts.
            }

            string itemNames = items.Count > 0 ? "You found " : "No items found";

            for (int i = 0; i < items.Count; i++)
            {
                itemNames += items[i].Data.Name;

                if (i < items.Count - 1)
                {
                    itemNames += ", ";
                }
            }

            messageBox = GameUIController.CurrentScreen.AddUIElement(new UIMessageBox(
                MainGame.Singleton.Content.Load<SpriteFont>("Fonts/Arial"),
                itemNames,
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
