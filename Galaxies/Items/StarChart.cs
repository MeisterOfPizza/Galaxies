using Galaxies.Controllers;
using Galaxies.Datas;
using Galaxies.Datas.Items;
using Galaxies.Datas.Space;
using Galaxies.Extensions;
using Galaxies.Space;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Items
{

    class StarChart : Item
    {

        private StarChartItemData data;

        public override bool CanSell { get { return false; } }

        public override bool CanUse { get { return true; } }

        public override Texture2D ItemIcon { get { return ContentHelper.GetSprite("Sprites/UI/star-chart-icon"); } }

        public StarChart(StarChartItemData data, Inventory inventory) : base(data, inventory)
        {
            this.data = data;
        }

        public void Use()
        {
            Inventory.RemoveItem(this);

            GalaxyController.AddVisitable(new PlanetarySystem(DataController.LoadData<PlanetarySystemData>(data.PlanetarySystemId, DataFileType.PlanetarySystems)));
        }

    }

}
