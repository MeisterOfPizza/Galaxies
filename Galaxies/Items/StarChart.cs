using Galaxies.Datas.Items;

namespace Galaxies.Items
{

    class StarChart : Item
    {

        private StarChartItemData data;

        public override bool CanSell { get { return false; } }

        public StarChart(StarChartItemData data, Inventory inventory) : base(data, inventory)
        {
            this.data = data;
        }

    }

}
