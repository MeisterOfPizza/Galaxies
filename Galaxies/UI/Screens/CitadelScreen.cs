using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Galaxies.UI.Special;
using Galaxies.Core;
using Microsoft.Xna.Framework;
using Galaxies.Controllers;
using Galaxies.Items;
using Galaxies.Economy;
using Galaxies.UI.Elements;
using Galaxies.Extensions;
using Galaxies.UIControllers;

namespace Galaxies.UI.Screens
{

    class CitadelScreen : Screen
    {

        UIInventory playerInventory;
        UIInventory merchantInventory;

        public override void CreateUI(ContentManager content)
        {
            Vector2 inventorySize = new Vector2(800, 600);

            //Player's inventory
            playerInventory = AddUIElement(new UIInventory(
                new Transform(Transform.SetPosition(Alignment.MiddleRight, new Vector2(-100, 0), inventorySize), inventorySize),
                this,
                PlayerController.Player.Inventory.Items,
                "Player",
                false
                ));

            //Merchant's inventory
            merchantInventory = AddUIElement(new UIInventory(
                new Transform(Transform.SetPosition(Alignment.MiddleLeft, new Vector2(100, 0), inventorySize), inventorySize),
                this,
                PlayerController.Player.Inventory.Items,
                "Merchant",
                false
                ));

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

            //Add the Top Info bar:
            AddUIElement(new UITopInfo(this));

            SetupItemButtons();
        }

        private void SetupItemButtons()
        {
            foreach (UIItem item in playerInventory.Items)
            {
                var uiPointer   = item; //Create a reference for the current UIItem in the iteration.
                var itemPointer = item.Item; //Create a reference for the current Item in the iteration.

                EventArg3<Item, Trader, Trader> onSellClicked 
                    = new EventArg3<Item, Trader, Trader>(MerchantController.TradeItem, itemPointer, MerchantController.Merchant, PlayerController.Player);

                //Create the sell button, allowing the player to sell the item to the merchant.
                //Also, swap the UIItem after it's sold/bought to the opposite UI Inventory (Player => Merchant and Merchant => Player).
                item.CreateSellButton(
                    new EventArgList(
                        onSellClicked,
                        new EventArg3<UIItem, bool, EventArg3<Item, Trader, Trader>>(SwapUIItems, uiPointer, true, onSellClicked)
                        ));
            }

            foreach (UIItem item in merchantInventory.Items)
            {
                var uiPointer   = item; //Create a reference for the current UIItem in the iteration.
                var itemPointer = item.Item; //Create a reference for the current Item in the iteration.

                EventArg3<Item, Trader, Trader> onPurchaseClicked 
                    = new EventArg3<Item, Trader, Trader>(MerchantController.TradeItem, itemPointer, PlayerController.Player, MerchantController.Merchant);

                //Create the purchase button, allowing the player to buy the item from the merchant.
                //Also, swap the UIItem after it's sold/bought to the opposite UI Inventory (Player => Merchant and Merchant => Player).
                item.CreatePurchaseButton(
                    new EventArgList(
                        onPurchaseClicked,
                        new EventArg3<UIItem, bool, EventArg3<Item, Trader, Trader>>(SwapUIItems, uiPointer, false, onPurchaseClicked)
                        ));
            }
        }

        private void SwapUIItems(UIItem uiItem, bool fromPlayer, EventArg3<Item, Trader, Trader> tradeEvent)
        {
            //Remove the old UIItem:
            if (fromPlayer) playerInventory.ItemGrid.RemoveUIElement(uiItem);
            else            merchantInventory.ItemGrid.RemoveUIElement(uiItem);

            //Assign a new instance to the reference that looks the same etc.:
            uiItem = new UIItem(new Transform(uiItem.Transform.Size), uiItem.Sprite, this, uiItem.Item);

            //Swap the Trader references in the EventArg:
            tradeEvent.SetArguments(
                tradeEvent.Arg1, //Keep the item reference
                tradeEvent.Arg3, //Switch reference
                tradeEvent.Arg2); //Switch reference

            //Create a new event list where the new swapped trade event and an entirely new EventArg3 can be created.
            EventArgList onButtonClickEvents = new EventArgList(
                tradeEvent,
                new EventArg3<UIItem, bool, EventArg3<Item, Trader, Trader>>(SwapUIItems, uiItem, !fromPlayer /* We want to trigger the opposite reaction */, tradeEvent)
                );

            //Create the purchase/sell button whether or not it came from the player's inventory.
            if (fromPlayer) uiItem.CreatePurchaseButton(onButtonClickEvents);
            else            uiItem.CreateSellButton(onButtonClickEvents);

            //Add the new UIItem to the right grid:
            if (fromPlayer) merchantInventory.ItemGrid.AddUIElement(uiItem);
            else            playerInventory.ItemGrid.AddUIElement(uiItem);
        }

    }

}
