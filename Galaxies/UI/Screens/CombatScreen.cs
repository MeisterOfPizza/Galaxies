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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            CombatController.Battlefield.Player.Draw(spriteBatch);
            CombatController.Battlefield.Enemy.Draw(spriteBatch);
        }

    }

}
