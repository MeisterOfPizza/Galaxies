using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Entities;
using Galaxies.Extensions;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Galaxies.Combat
{

    class Battlefield
    {

        /// <summary>
        /// The attacker, this is the player.
        /// </summary>
        public PlayerShip Player { get; private set; }

        /// <summary>
        /// The enemy that the player is fighting against.
        /// </summary>
        public EnemyShip Enemy { get; private set; }

        private bool AwaitEventCallbacks { get; set; }
        private bool PlayerTurn          { get; set; }

        private List<Bullet> Bullets { get; set; }

        private EventArg0 PlayerShotEvent;
        private EventArg0 EnemyShotEvent;

        public Battlefield(PlayerShip player, EnemyShip enemy)
        {
            this.Player = player;
            this.Enemy  = enemy;

            this.Bullets = new List<Bullet>();

            PlayerShotEvent = new EventArg0(AttackEnemy, EndAwaitEventCallbacks, EndTurn);
            EnemyShotEvent  = new EventArg0(AttackPlayer, EndAwaitEventCallbacks, EndTurn);
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
                GameUIController.CreatePlanetarySystemScreen();
                PlanetEventController.TriggerNextEvent();

                return;
            }

            StartTurn();
        }

        #region Player inputs

        public void Player_Attack()
        {
            if (AwaitEventCallbacks) return; //Return if we're still waiting for an event callback.

            Player.HasShieldUp = false;

            if (Player.Energy > PlayerShip.FIRE_ENERGY_COST)
            {
                Player.TakeEnergy(); //Remove energy because the player shot.

                Bullets.Add(CreateBullet(0, Player.Position, new Vector2(50f, 0), Enemy, PlayerShotEvent));

                AwaitEventCallbacks = true;
            }
            else
            {
                EndTurn();
            }
        }

        public void Player_ShieldUp()
        {
            if (AwaitEventCallbacks) return; //Return if we're still waiting for an event callback.

            //If the player already has their shield up, then regen some energy:
            if (Player.HasShieldUp)
            {
                Player.RegenEnergy();
            }

            Player.HasShieldUp = true;

            EndTurn();
        }

        #endregion

        #region Enemy Ai

        private void Ai()
        {
            //HACK: Very poor combat Ai.

            if (Enemy.Energy < EnemyShip.FIRE_ENERGY_COST)
            {
                if (Enemy.HasShieldUp)
                {
                    Enemy.RegenEnergy();
                }

                Enemy.HasShieldUp = true;

                EndTurn();
            }
            else
            {
                Enemy.HasShieldUp = false;

                Enemy.TakeEnergy(); //Remove energy because the enemy shot.

                Bullets.Add(CreateBullet(180, Enemy.Position, new Vector2(-50f, 0), Player, EnemyShotEvent));
                
                AwaitEventCallbacks = true;
            }

            //TODO: Call UI?
        }

        #endregion

        #region Helpers

        #region Event callback helpers

        private void EndAwaitEventCallbacks()
        {
            AwaitEventCallbacks = false;
        }

        /// <summary>
        /// Attack the player (for event callbacks).
        /// </summary>
        private void AttackPlayer()
        {
            Enemy.Attack(Player);
        }

        /// <summary>
        /// Attack the enemy (for event callbacks).
        /// </summary>
        private void AttackEnemy()
        {
            Player.Attack(Enemy);
        }

        #endregion

        private Bullet CreateBullet(float rotation, Vector2 position, Vector2 speed, ShipEntity target, EventArg eventArg)
        {
            return new Bullet(SpriteHelper.BulletSprite, position, rotation, Color.White, new Vector2(100), speed, target, eventArg);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in Bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public void Update()
        {
            foreach (Bullet bullet in Bullets.ToArray())
            {
                bullet.Move();

                if (bullet.Destroyed)
                {
                    Bullets.Remove(bullet);
                }
            }
        }

        #endregion

    }

}
