using Galaxies.Datas.Enemies;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;

namespace Galaxies.Entities
{

    public sealed class FinalBossShip : EnemyShip
    {

        public FinalBossShip(EnemyShipData data, Vector2 size) : base(data, size)
        {

        }

        public override void Die()
        {
            base.Die();

            GameUIController.CreateVictoryScreen();
        }

    }

}
