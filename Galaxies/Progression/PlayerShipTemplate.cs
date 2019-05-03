using Galaxies.Core;
using Galaxies.Datas.Player;
using Galaxies.Entities;
using Galaxies.Extensions;
using Microsoft.Xna.Framework;

namespace Galaxies.Progression
{

    class PlayerShipTemplate
    {

        public string     Id       { get; private set; }
        public string     Name     { get; private set; }
        public PlayerShip Ship     { get; private set; }
        public bool       Unlocked { get; set; }
        public int        Price    { get; private set; }

        public PlayerShipTemplate(PlayerShipTemplateData templateData, bool unlocked)
        {
            this.Id       = templateData.Id;
            this.Name     = templateData.Name;
            this.Unlocked = unlocked;
            this.Price    = templateData.Price;

            this.Ship = new PlayerShip(
                new Transform(new Vector2(100)),
                ContentHelper.GetSprite(templateData.SpriteName),
                templateData.BaseShipStats
                );
        }

    }

}
