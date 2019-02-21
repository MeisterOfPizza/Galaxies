using Galaxies.Combat;
using Galaxies.Entities;
using Galaxies.Space.Events;
using Microsoft.Xna.Framework;

namespace Galaxies.Controllers
{

    static class CombatController
    {

        public static Battlefield Battlefield { get; private set; }

        public static void StartBattle(CombatPlanetEvent @event)
        {
            Battlefield = new Battlefield(PlayerShip.Singleton, new EnemyShip(@event.EnemyShipData, new Vector2(100)));
        }

        public static void Update()
        {

        }

    }

}
