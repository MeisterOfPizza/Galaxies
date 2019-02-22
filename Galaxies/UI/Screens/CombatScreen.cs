using Galaxies.Controllers;
using Galaxies.UI.Elements;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Screens
{

    class CombatScreen : Screen
    {

        UIText playerStats;
        UIText enemyStats;

        public override void CreateUI(ContentManager content)
        {
            CombatController.Battlefield.Player.Position = new Vector2(GameUIController.WidthPercent(0.1f), GameUIController.HeightPercent(0.5f));
            CombatController.Battlefield.Enemy.Position  = new Vector2(GameUIController.WidthPercent(0.9f), GameUIController.HeightPercent(0.5f));

            var arial = content.Load<SpriteFont>("Fonts/Arial");
            var actionBackground = content.Load<Texture2D>("Sprites/UI/Column");

            AddUIElement(new UIButton(
                arial,
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
                arial,
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

            //Health, shield and energy text for the player:
            playerStats = AddUIElement(new UIText(
                arial,
                "Health: ",
                TextAlign.MiddleCenter,
                5,
                new Vector2(GameUIController.WidthPercent(0.1f), GameUIController.HeightPercent(0.3f)),
                0,
                Color.White,
                new Vector2(400, 100),
                this));

            //Health, shield and energy text for the enemy:
            enemyStats = AddUIElement(new UIText(
                arial,
                "Health: ",
                TextAlign.MiddleCenter,
                5,
                new Vector2(GameUIController.WidthPercent(0.7f), GameUIController.HeightPercent(0.3f)),
                0,
                Color.White,
                new Vector2(400, 100),
                this));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            playerStats.Text = "Health: " + CombatController.Battlefield.Player.Health + "\nShield: " + CombatController.Battlefield.Player.Shield + "\nEnergy: " + CombatController.Battlefield.Player.Energy;

            enemyStats.Text = "Health: " + CombatController.Battlefield.Enemy.Health + "\nShield: " + CombatController.Battlefield.Enemy.Shield + "\nEnergy: " + CombatController.Battlefield.Enemy.Energy;

            base.Draw(spriteBatch);

            CombatController.Battlefield.Player.Draw(spriteBatch);
            CombatController.Battlefield.Enemy.Draw(spriteBatch);
        }

    }

}
