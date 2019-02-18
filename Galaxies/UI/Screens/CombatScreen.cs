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

        UIText playerHealth;
        UIText playerEnergy;

        UIText enemyHealth;
        UIText enemyEnergy;

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

            //Health and energy for the player:
            playerHealth = AddUIElement(new UIText(
                arial,
                "Health: ",
                TextAlign.MiddleCenter,
                5,
                new Vector2(GameUIController.WidthPercent(0.1f), GameUIController.HeightPercent(0.3f)),
                0,
                Color.White,
                new Vector2(250, 100),
                this));

            playerEnergy = AddUIElement(new UIText(
                arial,
                "Energy: ",
                TextAlign.MiddleCenter,
                5,
                new Vector2(GameUIController.WidthPercent(0.3f), GameUIController.HeightPercent(0.3f)),
                0,
                Color.White,
                new Vector2(250, 100),
                this));

            //Health and energy for the enemy:
            enemyHealth = AddUIElement(new UIText(
                arial,
                "Health: ",
                TextAlign.MiddleCenter,
                5,
                new Vector2(GameUIController.WidthPercent(0.7f), GameUIController.HeightPercent(0.3f)),
                0,
                Color.White,
                new Vector2(250, 100),
                this));

            enemyEnergy = AddUIElement(new UIText(
                arial,
                "Energy: ",
                TextAlign.MiddleCenter,
                5,
                new Vector2(GameUIController.WidthPercent(0.9f), GameUIController.HeightPercent(0.3f)),
                0,
                Color.White,
                new Vector2(250, 100),
                this));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            playerHealth.Text = "Health: " + CombatController.Battlefield.Player.Health;
            playerEnergy.Text = "Energy: " + CombatController.Battlefield.Player.Energy;

            enemyHealth.Text = "Health: " + CombatController.Battlefield.Enemy.Health;
            enemyEnergy.Text = "Energy: " + CombatController.Battlefield.Enemy.Energy;

            base.Draw(spriteBatch);

            CombatController.Battlefield.Player.Draw(spriteBatch);
            CombatController.Battlefield.Enemy.Draw(spriteBatch);
        }

    }

}
