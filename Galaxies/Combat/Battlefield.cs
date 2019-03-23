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

        private EventArgList playerShotEvent;
        private EventArgList enemyShotEvent;

        public Battlefield(PlayerShip player, EnemyShip enemy)
        {
            this.Player = player;
            this.Enemy  = enemy;

            Player.Transform.Position = new Vector2(GameUIController.WidthPercent(0.1f), GameUIController.HeightPercent(0.5f));
            Enemy.Transform.Position  = new Vector2(GameUIController.WidthPercent(0.9f), GameUIController.HeightPercent(0.5f));

            this.Bullets = new List<Bullet>();

            playerShotEvent = new EventArgList(new EventArg1<ShipEntity>(Player.Attack, Enemy), new EventArg0(() => AwaitEventCallbacks = false, EndTurn));
            enemyShotEvent  = new EventArgList(new EventArg1<ShipEntity>(Enemy.Attack, Player), new EventArg0(() => AwaitEventCallbacks = false, EndTurn));

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
                GameUIController.CreateGameOverScreen();

                return;
            }
            else if (!Enemy.IsAlive)
            {
                GameUIController.CreatePlanetarySystemScreen(new EventArg0(PlanetEventController.TriggerNextEvent)); //Exit the combat screen.

                return;
            }

            StartTurn();
        }

        #region Player inputs

        public void Player_Attack()
        {
            if (AwaitEventCallbacks) return; //Return if we're still waiting for an event callback.

            if (Player.HasShieldUp)
            {
                AudioController.PlaySoundEffect("shield-down");
                Player.HasShieldUp = false;
                playerShieldEffect.Visable = false;
            }

            if (Player.Energy >= PlayerShip.FIRE_ENERGY_COST)
            {
                Player.TakeEnergy(); //Remove energy because the player shot.

                Bullets.Add(CreateBullet(0, Player.Transform.Position, new Vector2(50f, 0), Enemy, playerShotEvent));

                //Play sound effect:
                AudioController.PlaySoundEffect("laser");

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

            if (!Player.HasShieldUp)
            {
                AudioController.PlaySoundEffect("shield-up");
                Player.HasShieldUp = true;
                playerShieldEffect.Visable = true;
            }

            EndTurn();
        }

        #endregion

        #region Enemy Ai

        private void Ai()
        {
            //HACK: Very poor combat Ai.

            if (Enemy.Energy < EnemyShip.FIRE_ENERGY_COST) //Do they need to recharge energy (they don't have enough energy to shoot)?
            {
                if (Enemy.HasShieldUp)
                {
                    Enemy.RegenEnergy();
                }

                if (!Enemy.HasShieldUp)
                {
                    AudioController.PlaySoundEffect("shield-up");
                    Enemy.HasShieldUp = true;
                    enemyShieldEffect.Visable = true;
                }

                EndTurn();
            }
            else
            {
                if (Enemy.HasShieldUp)
                {
                    AudioController.PlaySoundEffect("shield-down");
                    Enemy.HasShieldUp = false;
                    enemyShieldEffect.Visable = false;
                }

                Enemy.TakeEnergy(); //Remove energy because the enemy shot.

                Bullets.Add(CreateBullet(180, Enemy.Transform.Position, new Vector2(-50f, 0), Player, enemyShotEvent));

                //Play sound effect:
                AudioController.PlaySoundEffect("laser");
                
                AwaitEventCallbacks = true;
            }
        }

        #endregion

        #region Helpers

        private Bullet CreateBullet(float rotation, Vector2 position, Vector2 speed, ShipEntity target, EventArg onHit)
        {
            return new Bullet(new Transform(position, new Vector2(100), rotation), SpriteHelper.Bullet_Sprite, speed, target, onHit);
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
