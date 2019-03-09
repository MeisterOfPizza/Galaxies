using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Special;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Screens
{

    class MenuScreen : Screen
    {

        UIColumn uiMenu;
        UISaveFiles uiSaveFiles;

        public override void CreateUI(ContentManager content)
        {
            var textBackground = content.Load<Texture2D>("Sprites/UI/Column");

            uiMenu = AddUIElement(new UIColumn(
                new Transform(Alignment.MiddleCenter, new Vector2(250, 400)),
                content.Load<Texture2D>("Sprites/Transparent"),
                this,
                new Vector4(5, 0, 5, 0),
                new Vector2(0, 5)
                ));

            uiMenu.AddUIElements(
                new UIButton(new Transform(new Vector2(250, 100)), SpriteHelper.Arial_Font, "New Game", TextAlign.MiddleCenter, 5, textBackground, null, this),
                new UIButton(new Transform(new Vector2(250, 100)), SpriteHelper.Arial_Font, "Load Game", TextAlign.MiddleCenter, 5, textBackground, new EventArg0(OpenUISaveFiles), this),
                new UIButton(new Transform(new Vector2(250, 100)), SpriteHelper.Arial_Font, "Options", TextAlign.MiddleCenter, 5, textBackground, null, this),
                new UIButton(new Transform(new Vector2(250, 100)), SpriteHelper.Arial_Font, "Exit", TextAlign.MiddleCenter, 5, textBackground, new EventArg0(MainGame.Singleton.Exit), this)
                );

            uiSaveFiles = AddUIElement(new UISaveFiles(
                new Transform(Alignment.MiddleCenter, new Vector2(250, 600)),
                this,
                false,
                "Load",
                new EventArg0(OpenMenu)
                ));

            uiSaveFiles.Visable = false;
        }

        private void OpenUISaveFiles()
        {
            uiMenu.Visable = false;
            uiSaveFiles.Open();
        }

        public void OpenMenu()
        {
            uiMenu.Visable = true;
        }

    }

}
