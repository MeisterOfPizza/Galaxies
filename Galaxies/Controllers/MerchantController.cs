using Galaxies.Datas.Merchant;
using Galaxies.Economy;
using Galaxies.Items;

namespace Galaxies.Controllers
{

    static class MerchantController
    {

        public static Trader Merchant { get; private set; }

        public static void CreateNewMerchant()
        {
            Merchant = new Trader(null, new Balance(int.MaxValue / 2));
            Merchant.Inventory = new Inventory(Merchant);

            SetItems();
        }

        /// <summary>
        /// Sets the items randomly.
        /// </summary>
        private static void SetItems()
        {
            var items = DataController.LoadData<MerchantShopData>("default", DataFileType.Merchant).GetItems(Merchant.Inventory);

            Merchant.Inventory.AddItems(items);
        }

        public static void TradeItem(Item tradedItem, Trader buyer, Trader seller)
        {
            if (buyer.Balance.Extract(tradedItem.Data.GalacticGoldValue))
            {
                tradedItem.ChangeInventory(buyer.Inventory);

                seller.Balance.Deposit(tradedItem.Data.GalacticGoldValue);
            }
        }

    }

}
