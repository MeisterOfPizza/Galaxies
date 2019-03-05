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

namespace Galaxies.UI.Screens
{

    class CitadelScreen : Screen
    {

        UIInventory playerInventory;
        UIInventory merchantInventory;

        public override void CreateUI(ContentManager content)
        {
            //Player's inventory
            playerInventory = AddUIElement(new UIInventory(
                new Transform(new Vector2(800, 600)),
                this,
                PlayerController.Player.Inventory.Items
                ));

            //Merchant's inventory
            merchantInventory = AddUIElement(new UIInventory(
                new Transform(new Vector2(800, 600)),
                this,
                PlayerController.Player.Inventory.Items
                ));
        }

        private void SetupItemButtons()
        {
            foreach (UIItem item in playerInventory.Items)
            {
                var pointer = item.Item;

                //Create the sell button, allowing the player to sell the item to the merchant.
                item.CreateSellButton(new EventArg3<Item, Trader, Trader>(MerchantController.TradeItem, pointer, MerchantController.Merchant, PlayerController.Player));
            }

            foreach (UIItem item in merchantInventory.Items)
            {
                var pointer = item.Item;

                //Create the purchase button, allowing the player to buy the item from the merchant.
                item.CreatePurchaseButton(new EventArg3<Item, Trader, Trader>(MerchantController.TradeItem, pointer, PlayerController.Player, MerchantController.Merchant));
            }
        }

    }

}
