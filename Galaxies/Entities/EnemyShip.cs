using Galaxies.Controllers;
using Galaxies.Datas.Enemies;
using Galaxies.Datas.Items;
using Galaxies.Extensions;
using Galaxies.Items;
using Microsoft.Xna.Framework;

namespace Galaxies.Entities
{

    class EnemyShip : ShipEntity
    {

        private EnemyShipData Data { get; set; }

        public EnemyShip(EnemyShipData data, Vector2 size) : base(SpriteHelper.GetSprite(data.SpriteName), Vector2.Zero, 0, data.Color.GetColor(), size, Vector2.Zero, data.BaseShipStats)
        {
            this.Data = data;

            foreach (string id in Data.ShipUpgradeIds)
            {
                //Inventory.AddItem(new ShipUpgrade(DataController.LoadData<ShipUpgradeItemData>(id, DataFileType.Items), Inventory));
            }
        }

    }

}
