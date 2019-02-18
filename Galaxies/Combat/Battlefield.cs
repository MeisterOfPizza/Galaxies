using Galaxies.Controllers;
using Galaxies.Entities;

namespace Galaxies.Combat
{

    class Battlefield
    {

        /// <summary>
        /// The attacker, this is the player.
        /// </summary>
        public ShipEntity Player { get; private set; }

        /// <summary>
        /// The enemy that the player is fighting against.
        /// </summary>
        public ShipEntity Enemy { get; private set; }

        public bool PlayerTurn { get; private set; }

        public bool PlayerHasShieldUp { get; private set; }
        public bool EnemyHasShieldUp  { get; private set; }

        public Battlefield(PlayerShip player, ShipEntity enemy)
        {
            this.Player = player;
            this.Enemy  = enemy;
        }

        public void StartTurn()
        {
            PlayerTurn = !PlayerTurn;

            if (!PlayerTurn)
            {
                Ai();
            }
        }

        private void EndTurn()
        {
            if (!Player.IsAlive)
            {
                //TODO: End game

                return;
            }
            else if (!Enemy.IsAlive)
            {
                //TODO: Give item drops?
                PlanetEventController.TriggerNextEvent();

                return;
            }

            StartTurn();
        }

        #region Player inputs

        public void Player_Attack()
        {
            PlayerHasShieldUp = false;

            if (Player.Energy > PlayerShip.FIRE_ENERGY_COST)
            {
                Player.Attack(Enemy);
            }

            EndTurn();
        }

        public void Player_ShieldUp()
        {
            //If the player already has their shield up, then regen some energy:
            if (PlayerHasShieldUp)
            {
                Player.RegenEnergy();
            }

            PlayerHasShieldUp = true;

            EndTurn();
        }

        #endregion

        #region Enemy Ai

        private void Ai()
        {
            //TODO: Create a combat Ai.

            //Just attack the player for now:
            Enemy.Attack(Player);

            //TODO: Call UI?

            EndTurn();
        }

        #endregion

    }

}
