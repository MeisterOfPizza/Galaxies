using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Entities;
using Galaxies.Extensions;
using Galaxies.UI;
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

        private UIElement playerShieldEffect;
        private UIElement enemyShieldEffect;

        private EventArg0 playerShotEvent;
        private EventArg0 enemyShotEvent;

        public Battlefield(PlayerShip player, EnemyShip enemy)
        {
            this.Player = player;
            this.Enemy  = enemy;

            Player.Transform.Position = new Vector2(GameUIController.WidthPercent(0.1f), GameUIController.HeightPercent(0.5f));
            Enemy.Transform.Position  = new Vector2(GameUIController.WidthPercent(0.9f), GameUIController.HeightPercent(0.5f));

            this.Bullets = new List<Bullet>();

            playerShotEvent = new EventArg0(AttackEnemy, EndAwaitEventCallbacks, EndTurn);
            enemyShotEvent  = new EventArg0(AttackPlayer, EndAwaitEventCallbacks, EndTurn);

            playerShieldEffect = new UIElement(new Transform(Player.Transform.Position, Player.Transform.Size * 2), SpriteHelper.Shield_Sprite, GameUIController.CurrentScreen);
            enemyShieldEffect  = new UIElement(new Transform(Enemy.Transform.Position, Enemy.Transform.Size * 2), SpriteHelper.Shield_Sprite, GameUIController.CurrentScreen);

            playerShieldEffect.Visable = false;
            enemyShieldEffect.Visable  = false;
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
                Player.RefillStats(); //Refill player's stats to ready them for the next battle.

                GameUIController.CreatePlanetarySystemScreen(); //Exit the combat screen.
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
            playerShieldEffect.Visable = false;

            if (Player.Energy >= PlayerShip.FIRE_ENERGY_COST)
            {
                Player.TakeEnergy(); //Remove energy because the player shot.

                Bullets.Add(CreateBullet(0, Player.Transform.Position, new Vector2(50f, 0), Enemy, playerShotEvent));

                AwaitEventCallbacks = true;
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
            playerShieldEffect.Visable = true;

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
                enemyShieldEffect.Visable = true;

                EndTurn();
            }
            else
            {
                Enemy.HasShieldUp = false;
                enemyShieldEffect.Visable = false;

                Enemy.TakeEnergy(); //Remove energy because the enemy shot.

                Bullets.Add(CreateBullet(180, Enemy.Transform.Position, new Vector2(-50f, 0), Player, enemyShotEvent));
                
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
            return new Bullet(new Transform(position, new Vector2(100), rotation), SpriteHelper.Bullet_Sprite, speed, target, eventArg);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in Bullets)
            {
                bullet.Draw(spriteBatch);
            }

            playerShieldEffect.Draw(spriteBatch);
            enemyShieldEffect.Draw(spriteBatch);
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
