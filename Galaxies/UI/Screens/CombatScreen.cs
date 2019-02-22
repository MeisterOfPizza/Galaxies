using Galaxies.Controllers;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Screens
{

    class CombatScreen : Screen
    {

        UIProgressBar playerHealthBar;
        UIProgressBar playerShieldBar;
        UIProgressBar playerEnergyBar;

        UIProgressBar enemyHealthBar;
        UIProgressBar enemyShieldBar;
        UIProgressBar enemyEnergyBar;

        public override void CreateUI(ContentManager content)
        {
            var actionBackground = content.Load<Texture2D>("Sprites/UI/Column");

            AddUIElement(new UIButton(
                SpriteHelper.Arial_Font,
                "FIRE",
                TextAlign.MiddleCenter,
                5,
                actionBackground,
                new Vector2(GameUIController.WidthPercent(0.1f), GameUIController.HeightPercent(0.9f)),
                0,
                Color.White,
                new Vector2(250, 100),
                CombatController.Battlefield.Player_Attack,
                this));

            AddUIElement(new UIButton(
                SpriteHelper.Arial_Font,
                "SHIELD UP",
                TextAlign.MiddleCenter,
                5,
                actionBackground,
                new Vector2(GameUIController.WidthPercent(0.3f), GameUIController.HeightPercent(0.9f)),
                0,
                Color.White,
                new Vector2(250, 100),
                CombatController.Battlefield.Player_ShieldUp,
                this));

            //Health, shield and energy bars for the player:
            playerHealthBar = AddUIElement(new UIProgressBar(
                SpriteHelper.Box4x4_Sprite,
                SpriteHelper.Box4x4_Sprite,
                new Vector2(GameUIController.WidthPercent(0.15f), GameUIController.HeightPercent(0.3f)),
                0,
                Color.Red,
                Color.LawnGreen,
                new Vector2(300, 25),
                Vector2.Zero,
                0,
                this));

            playerShieldBar = AddUIElement(new UIProgressBar(
                SpriteHelper.Box4x4_Sprite,
                SpriteHelper.Box4x4_Sprite,
                new Vector2(GameUIController.WidthPercent(0.15f), GameUIController.HeightPercent(0.35f)),
                0,
                Color.DarkBlue,
                Color.Cyan,
                new Vector2(300, 25),
                Vector2.Zero,
                0,
                this));

            playerEnergyBar = AddUIElement(new UIProgressBar(
                SpriteHelper.Box4x4_Sprite,
                SpriteHelper.Box4x4_Sprite,
                new Vector2(GameUIController.WidthPercent(0.15f), GameUIController.HeightPercent(0.4f)),
                0,
                Color.Black,
                Color.Yellow,
                new Vector2(300, 25),
                Vector2.Zero,
                0,
                this));

            //Name text for the enemy:
            AddUIElement(new UIText(
                SpriteHelper.Arial_Font,
                CombatController.Battlefield.Enemy.Data.Name,
                TextAlign.MiddleCenter,
                5,
                new Vector2(GameUIController.WidthPercent(0.85f), GameUIController.HeightPercent(0.25f)),
                0,
                Color.White,
                new Vector2(250, 100),
                this));

            //Health, shield and energy bars for the enemy:
            enemyHealthBar = AddUIElement(new UIProgressBar(
                SpriteHelper.Box4x4_Sprite,
                SpriteHelper.Box4x4_Sprite,
                new Vector2(GameUIController.WidthPercent(0.85f), GameUIController.HeightPercent(0.3f)),
                0,
                Color.Red,
                Color.LawnGreen,
                new Vector2(300, 25),
                Vector2.Zero,
                0,
                this));

            enemyShieldBar = AddUIElement(new UIProgressBar(
                SpriteHelper.Box4x4_Sprite,
                SpriteHelper.Box4x4_Sprite,
                new Vector2(GameUIController.WidthPercent(0.85f), GameUIController.HeightPercent(0.35f)),
                0,
                Color.DarkBlue,
                Color.Cyan,
                new Vector2(300, 25),
                Vector2.Zero,
                0,
                this));

            enemyEnergyBar = AddUIElement(new UIProgressBar(
                SpriteHelper.Box4x4_Sprite,
                SpriteHelper.Box4x4_Sprite,
                new Vector2(GameUIController.WidthPercent(0.85f), GameUIController.HeightPercent(0.4f)),
                0,
                Color.Black,
                Color.Yellow,
                new Vector2(300, 25),
                Vector2.Zero,
                0,
                this));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            playerHealthBar.Progress = CombatController.Battlefield.Player.Health / (float)CombatController.Battlefield.Player.MaxHealth;
            playerShieldBar.Progress = CombatController.Battlefield.Player.Shield / (float)CombatController.Battlefield.Player.MaxShield;
            playerEnergyBar.Progress = CombatController.Battlefield.Player.Energy / (float)CombatController.Battlefield.Player.MaxEnergy;

            enemyHealthBar.Progress = CombatController.Battlefield.Enemy.Health / (float)CombatController.Battlefield.Enemy.MaxHealth;
            enemyShieldBar.Progress = CombatController.Battlefield.Enemy.Shield / (float)CombatController.Battlefield.Enemy.MaxShield;
            enemyEnergyBar.Progress = CombatController.Battlefield.Enemy.Energy / (float)CombatController.Battlefield.Enemy.MaxEnergy;

            base.Draw(spriteBatch);

            CombatController.Battlefield.Player.Draw(spriteBatch);
            CombatController.Battlefield.Enemy.Draw(spriteBatch);
        }

    }

}
