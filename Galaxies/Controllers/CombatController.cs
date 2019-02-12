using Galaxies.Combat;
using Galaxies.Datas.Enemies;
using Galaxies.Entities;
using Galaxies.Extensions;
using Galaxies.Space.Events;
using Microsoft.Xna.Framework;

namespace Galaxies.Controllers
{

    static class CombatController
    {

        public static Battlefield Battlefield { get; private set; }

        public static void StartBattle(CombatPlanetEvent @event)
        {
            string enemyId = @event.data.EnemyIds[Random.Next(@event.data.EnemyIds.Length)];

            Battlefield = new Battlefield(PlayerShip.Singleton, new EnemyShip(DataController.LoadData<EnemyShipData>(enemyId, DataFileType.Enemies), new Vector2(100)));
        }

        public static void Update()
        {

        }

    }

}
