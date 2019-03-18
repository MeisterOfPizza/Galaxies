using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Economy;
using Galaxies.Items;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Special
{

    /// <summary>
    /// Fixed <see cref="UIGroup"/> with the player and the Citadel's merchant.
    /// </summary>
    class UIShop : UIGroup
    {

        UIInventory uiPlayerInventory;
        UIInventory uiMerchantInventory;

        public UIShop(Transform transform, Screen screen) : base(transform, null, screen)
        {
            Vector2 inventorySize = new Vector2(800, 600);

            //Player's inventory
            uiPlayerInventory = AddUIElement(new UIInventory(
                new Transform(new Vector2(450, 0), inventorySize),
                screen,
                PlayerController.Player.Inventory.Items,
                "Player",
                false,
                null
                ));

            //Merchant's inventory
            uiMerchantInventory = AddUIElement(new UIInventory(
                new Transform(new Vector2(-450, 0), inventorySize),
                screen,
                MerchantController.Merchant.Inventory.Items,
                "Merchant",
                false,
                null
                ));

            SetupItemButtons();
        }

        private void SetupItemButtons()
        {
            foreach (UIItem item in uiPlayerInventory.Items)
            {
                if (item.Item.CanSell)
                {
                    var uiPointer = item; //Create a reference for the current UIItem in the iteration.
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
            }

            foreach (UIItem item in uiMerchantInventory.Items)
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

        /// <summary>
        /// Swap <see cref="UIItem"/>s from one <see cref="UIInventory"/> to the other.
        /// </summary>
        private void SwapUIItems(UIItem uiItem, bool fromPlayer, EventArg3<Item, Trader, Trader> tradeEvent)
        {
            // Check if the trade was successful:
            // (I know this is a very ugly way of doing it, but I don't want to introduce new EventArg(s) with boolean as their return type...)
            // If we are transfering the item FROM the player, then the player shouldn't have the item in the inventory if the trade was successful and vice versa for the merchant.
            if ((fromPlayer && !PlayerController.Player.Inventory.Items.Contains(uiItem.Item)) || (!fromPlayer && PlayerController.Player.Inventory.Items.Contains(uiItem.Item)))
            {
                //Remove the old UIItem:
                if (fromPlayer) uiPlayerInventory.ItemGrid.RemoveUIElement(uiItem);
                else            uiMerchantInventory.ItemGrid.RemoveUIElement(uiItem);

                //Assign a new instance to the reference that looks the same etc.:
                uiItem = new UIItem(new Transform(uiItem.Transform.Size), uiItem.Sprite, screen, uiItem.Item);

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
                if (fromPlayer)               uiItem.CreatePurchaseButton(onButtonClickEvents);
                else if (uiItem.Item.CanSell) uiItem.CreateSellButton(onButtonClickEvents);

                //Add the new UIItem to the right grid:
                if (fromPlayer) uiMerchantInventory.ItemGrid.AddUIElement(uiItem);
                else            uiPlayerInventory.ItemGrid.AddUIElement(uiItem);
            }
        }

    }

}
