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

            var container = AddUIElement(new UIColumn(content.Load<Texture2D>("Sprites/Transparent"), GameUIController.Center(100, 500), 0, Color.White, null, this, 5, 5));
            container.SetDrawSize(100, 500);
            container.AddUIElements(250, 100,
                new UIButton(arialFont, "New Game", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, null, this),
                new UIButton(arialFont, "Load Game", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, null, this),
                new UIButton(arialFont, "Options", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, null, this),
                new UIButton(arialFont, "Exit", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, MainGame.Singleton.Exit, this)
                );
        }

    }

}
