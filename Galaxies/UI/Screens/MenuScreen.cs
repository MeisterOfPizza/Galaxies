using Galaxies.UI.Elements;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Screens
{

    class MenuScreen : Screen
    {

        public override void CreateUI(ContentManager content)
        {
            var arialFont      = content.Load<SpriteFont>("Fonts/Arial");
            var textBackground = content.Load<Texture2D>("Sprites/UI/Column");

            var container = AddUIElement(new UIColumn(content.Load<Texture2D>("Sprites/Transparent"), GameUIController.Center(), 0, Color.White, new Vector2(100, 500), this, 5, 5));
            container.AddUIElements(
                new UIButton(arialFont, "New Game", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, new Vector2(250, 100), null, this),
                new UIButton(arialFont, "Load Game", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, new Vector2(250, 100), null, this),
                new UIButton(arialFont, "Options", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, new Vector2(250, 100), null, this),
                new UIButton(arialFont, "Exit", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, new Vector2(250, 100), MainGame.Singleton.Exit, this)
                );
        }

    }

}
