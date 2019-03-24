using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Special;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Screens
{

    class MenuScreen : Screen
    {

        UIColumn         uiMenu;
        UISaveFiles      uiSaveFiles;
        UICreateSaveFile uiCreateSaveFile;

        public override void CreateUI()
        {
            AddUIElement(new UIBackgroundAnimation(
                new Transform(Alignment.MiddleCenter, GameUIController.WindowSize),
                ContentHelper.Space_Background_Animation_2,
                this
                ));

            var textBackground = ContentHelper.GetSprite("Sprites/UI/Column");

            uiMenu = AddUIElement(new UIColumn(
                new Transform(Alignment.MiddleCenter, new Vector2(250, 400)),
                null,
                this,
                new Vector4(5, 0, 5, 0),
                new Vector2(0, 5)
                ));

            uiMenu.AddUIElements(
                new UIButton(new Transform(new Vector2(250, 100)), ContentHelper.Arial_Font, "New Game", TextAlign.MiddleCenter, 5, textBackground, new EventArg0(ToggleUICreateSave), this),
                new UIButton(new Transform(new Vector2(250, 100)), ContentHelper.Arial_Font, "Load Game", TextAlign.MiddleCenter, 5, textBackground, new EventArg0(ToggleUISaveFiles), this),
                new UIButton(new Transform(new Vector2(250, 100)), ContentHelper.Arial_Font, "Options", TextAlign.MiddleCenter, 5, textBackground, null, this),
                new UIButton(new Transform(new Vector2(250, 100)), ContentHelper.Arial_Font, "Exit", TextAlign.MiddleCenter, 5, textBackground, new EventArg0(MainGame.Singleton.Exit), this)
                );

            uiSaveFiles = AddUIElement(new UISaveFiles(
                new Transform(Alignment.MiddleCenter, new Vector2(250, 600)),
                this,
                false,
                "Load",
                new EventArg0(ToggleUISaveFiles)
                ));

            uiSaveFiles.Visable = false;

            uiCreateSaveFile = AddUIElement(new UICreateSaveFile(
                new Transform(Alignment.MiddleCenter, new Vector2(500, 150)),
                ContentHelper.GetSprite("Sprites/UI/Column"),
                this,
                new EventArg0(ToggleUICreateSave)
                ));

            uiCreateSaveFile.Visable = false;
        }

        private void ToggleUISaveFiles()
        {
            uiMenu.Visable = !uiMenu.Visable;
            uiSaveFiles.Visable = !uiSaveFiles.Visable;
        }

        private void ToggleUICreateSave()
        {
            uiMenu.Visable = !uiMenu.Visable;
            uiCreateSaveFile.Visable = !uiCreateSaveFile.Visable;
        }

    }

}
