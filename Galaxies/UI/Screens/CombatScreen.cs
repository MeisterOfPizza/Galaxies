using Galaxies.Controllers;
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
            CombatController.Player.Position    = new Vector2(GameUIController.WidthPercent(0.1f), GameUIController.HeightPercent(0.5f));
            CombatController.EnemyShip.Position = new Vector2(GameUIController.WidthPercent(0.9f), GameUIController.HeightPercent(0.5f));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            CombatController.Player.Draw(spriteBatch);
            CombatController.EnemyShip.Draw(spriteBatch);
        }

    }

}
