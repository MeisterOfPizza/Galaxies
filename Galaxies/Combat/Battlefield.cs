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

        public bool TurnIsDone { get; private set; }

        public bool PlayerTurn { get; private set; }

        public Battlefield(PlayerShip player, ShipEntity enemy)
        {
            this.Player = player;
            this.Enemy  = enemy;
        }

        public void StartTurn()
        {
            TurnIsDone = false;

            if (!PlayerTurn)
            {
                Ai();
            }

            //TODO: Update UI?

            PlayerTurn = !PlayerTurn;
        }

        #region Player inputs

        public void Player_Attack()
        {

        }

        public void Player_ShieldUp()
        {

        }

        #endregion

        #region Enemy Ai

        private void Ai()
        {
            //TODO: Create a combat Ai.

            //Just attack the player for now:
            Enemy.Attack(Player);

            //TODO: Call UI?

            TurnIsDone = true;
        }

        #endregion

    }

}
