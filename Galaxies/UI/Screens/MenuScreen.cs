using Galaxies.Core;
using Galaxies.Extensions;
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
            var textBackground = content.Load<Texture2D>("Sprites/UI/Column");

            var container = AddUIElement(new UIColumn(content.Load<Texture2D>("Sprites/Transparent"), GameUIController.Center(), 0, Color.White, new Vector2(250, 410), this, new Vector4(5, 0, 5, 0), new Vector2(0, 5)));
            container.AddUIElements(
                new UIButton(SpriteHelper.Arial_Font, "New Game", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, new Vector2(250, 100), null, this),
                new UIButton(SpriteHelper.Arial_Font, "Load Game", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, new Vector2(250, 100), null, this),
                new UIButton(SpriteHelper.Arial_Font, "Options", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, new Vector2(250, 100), null, this),
                new UIButton(SpriteHelper.Arial_Font, "Exit", TextAlign.MiddleCenter, 5, textBackground, Vector2.Zero, 0, Color.White, new Vector2(250, 100), new EventArg0(MainGame.Singleton.Exit), this)
                );
        }

    }

}
