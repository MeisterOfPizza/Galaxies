using Galaxies.Combat;
using Galaxies.Space.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Controllers
{

    static class CombatController
    {

        public static Battlefield Battlefield { get; private set; }

        public static void StartBattle(CombatPlanetEvent @event)
        {
            Battlefield = new Battlefield(PlayerController.Ship, @event.EnemyShipData.GetEnemyShip(new Vector2(100)));
            Battlefield.StartTurn();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            Battlefield.Draw(spriteBatch);
        }

        public static void Update()
        {
            Battlefield.Update();
        }

    }

}
