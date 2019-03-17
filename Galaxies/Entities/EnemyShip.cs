using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Datas.Enemies;
using Galaxies.Datas.Items;
using Galaxies.Extensions;
using Galaxies.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Galaxies.Entities
{

    class EnemyShip : ShipEntity, IInventoryOwner
    {

        #region Constants

        public const int FIRE_ENERGY_COST = 10;

        #endregion

        public EnemyShipData Data { get; private set; }

        #region IInventoryOwner

        public Inventory Inventory { get; set; }

        #endregion

        public EnemyShip(EnemyShipData data, Vector2 size) : base(new Transform(size), SpriteHelper.GetSprite(data.SpriteName), Vector2.Zero, data.BaseShipStats)
        {
            this.SetColor(data.Color.GetColor());

            this.Inventory = new Inventory(this);

            this.Data = data;

            if (Data.ShipUpgradeIds != null)
            {
                foreach (string id in Data.ShipUpgradeIds)
                {
                    Inventory.AddItem(new ShipUpgrade(DataController.LoadData<ShipUpgradeItemData>(id, DataFileType.Items), Inventory));
                }
            }

            //Update the new stats:
            RefillStats();
        }

        protected void CalculateStats()
        {
            var shipUpgradeShipStats = Inventory.Items
                .Where(i => i.Data.ItemType == ItemType.ShipUpgrade)
                .Select(i => ((ShipUpgradeItemData)i.Data).ShipStats);

            int newHealth      = 0;
            int newShield      = 0;
            int newDamage      = 0;
            int newEnergy      = 0;
            int newEnergyRegen = 0;

            foreach (ShipStats stats in shipUpgradeShipStats)
            {
                newHealth      += stats.Health;
                newShield      += stats.Shield;
                newDamage      += stats.Damage;
                newEnergy      += stats.Energy;
                newEnergyRegen += stats.EnergyRegen;
            }

            modifiedStats = new ShipStats(newHealth, newShield, newDamage, newEnergy, newEnergyRegen);
        }

        public override void TakeEnergy()
        {
            Energy -= FIRE_ENERGY_COST;
        }

        #region IInventory

        public void InventoryChanged(Item changedItem)
        {
            //We only care to update stats if the changed item was of type ShipUpgrade.
            if (changedItem.Data.ItemType == ItemType.ShipUpgrade)
            {
                CalculateStats();
            }
        }

        public void InventoryChanged(IList<Item> changedItems)
        {
            //We only care to update stats if the changed item was of type ShipUpgrade,
            //however, we don't want to look through the whole list, so just update in case.
            CalculateStats();
        }

        #endregion

    }

}
