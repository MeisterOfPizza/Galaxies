using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

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

        public async override Task CreateUIAsync()
        {
            var actionBackground = SpriteHelper.GetSprite("Sprites/UI/Column");

            AddUIElement(new UIButton(
                new Transform(new Vector2(GameUIController.WidthPercent(0.1f), GameUIController.HeightPercent(0.9f)), new Vector2(250, 100)),
                SpriteHelper.Arial_Font,
                "FIRE",
                TextAlign.MiddleCenter,
                5,
                actionBackground,
                new EventArg0(CombatController.Battlefield.Player_Attack),
                this));

            AddUIElement(new UIButton(
                new Transform(new Vector2(GameUIController.WidthPercent(0.3f), GameUIController.HeightPercent(0.9f)), new Vector2(250, 100)),
                SpriteHelper.Arial_Font,
                "SHIELD UP",
                TextAlign.MiddleCenter,
                5,
                actionBackground,
                new EventArg0(CombatController.Battlefield.Player_ShieldUp),
                this));

            //Health, shield and energy bars for the player:
            playerHealthBar = AddUIElement(new UIProgressBar(
                new Transform(new Vector2(GameUIController.WidthPercent(0.15f), GameUIController.HeightPercent(0.3f)), new Vector2(300, 25)),
                SpriteHelper.Box4x4_Sprite,
                Color.Red,
                SpriteHelper.Box4x4_Sprite,
                Color.LawnGreen,
                Vector2.Zero,
                0,
                this));

            playerShieldBar = AddUIElement(new UIProgressBar(
                new Transform(new Vector2(GameUIController.WidthPercent(0.15f), GameUIController.HeightPercent(0.35f)), new Vector2(300, 25)),
                SpriteHelper.Box4x4_Sprite,
                Color.DarkBlue,
                SpriteHelper.Box4x4_Sprite,
                Color.Cyan,
                Vector2.Zero,
                0,
                this));

            playerEnergyBar = AddUIElement(new UIProgressBar(
                new Transform(new Vector2(GameUIController.WidthPercent(0.15f), GameUIController.HeightPercent(0.4f)), new Vector2(300, 25)),
                SpriteHelper.Box4x4_Sprite,
                Color.Black,
                SpriteHelper.Box4x4_Sprite,
                Color.Yellow,
                Vector2.Zero,
                0,
                this));

            //Name text for the enemy:
            AddUIElement(new UIText(
                new Transform(new Vector2(GameUIController.WidthPercent(0.85f), GameUIController.HeightPercent(0.25f)), new Vector2(250, 100)),
                SpriteHelper.Arial_Font,
                CombatController.Battlefield.Enemy.Data.Name,
                TextAlign.MiddleCenter,
                5,
                this));

            //Health, shield and energy bars for the enemy:
            enemyHealthBar = AddUIElement(new UIProgressBar(
                new Transform(new Vector2(GameUIController.WidthPercent(0.85f), GameUIController.HeightPercent(0.3f)), new Vector2(300, 25)),
                SpriteHelper.Box4x4_Sprite,
                Color.Red,
                SpriteHelper.Box4x4_Sprite,
                Color.LawnGreen,
                Vector2.Zero,
                0,
                this));

            enemyShieldBar = AddUIElement(new UIProgressBar(
                new Transform(new Vector2(GameUIController.WidthPercent(0.85f), GameUIController.HeightPercent(0.35f)), new Vector2(300, 25)),
                SpriteHelper.Box4x4_Sprite,
                Color.DarkBlue,
                SpriteHelper.Box4x4_Sprite,
                Color.Cyan,
                Vector2.Zero,
                0,
                this));

            enemyEnergyBar = AddUIElement(new UIProgressBar(
                new Transform(new Vector2(GameUIController.WidthPercent(0.85f), GameUIController.HeightPercent(0.4f)), new Vector2(300, 25)),
                SpriteHelper.Box4x4_Sprite,
                Color.Black,
                SpriteHelper.Box4x4_Sprite,
                Color.Yellow,
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
