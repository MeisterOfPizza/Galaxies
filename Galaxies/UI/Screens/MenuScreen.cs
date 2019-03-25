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

        UIGroup          uiMenu;
        UISaveFiles      uiSaveFiles;
        UICreateSaveFile uiCreateSaveFile;

        public override void CreateUI()
        {
            AddUIElement(new UIBackgroundAnimation(
                new Transform(Alignment.MiddleCenter, GameUIController.WindowSize),
                ContentHelper.Space_Background_Animation_2,
                this
                ));

            var textBackground = ContentHelper.Box4x4_Sprite;

            uiMenu = AddUIElement(new UIGroup(
                new Transform(Transform.CreatePosition(Alignment.BottomCenter, new Vector2(0, -20), new Vector2(1000, 50)), new Vector2(1000, 50)),
                null,
                this
                ));

            uiMenu.AddUIElements(
                //New game
                new UIButton(
                    new Transform(new Vector2(-390, 0), new Vector2(250, 50)),
                    ContentHelper.Arial_Font,
                    "New Game",
                    TextAlign.MiddleCenter,
                    0,
                    textBackground,
                    new EventArg0(ToggleUICreateSave),
                    this
                    ),
                //Load game
                new UIButton(
                    new Transform(new Vector2(-130, 0), new Vector2(250, 50)),
                    ContentHelper.Arial_Font,
                    "Load Game",
                    TextAlign.MiddleCenter,
                    0,
                    textBackground,
                    new EventArg0(ToggleUISaveFiles),
                    this
                    ),
                //Settings
                new UIButton(
                    new Transform(new Vector2(130, 0), new Vector2(250, 50)),
                    ContentHelper.Arial_Font,
                    "Settings",
                    TextAlign.MiddleCenter,
                    0,
                    textBackground,
                    new EventArg1<EventArg>(GameUIController.CreateSettingsScreen, null),
                    this
                    ),
                //Exit
                new UIButton(
                    new Transform(new Vector2(390, 0), new Vector2(250, 50)),
                    ContentHelper.Arial_Font,
                    "Exit",
                    TextAlign.MiddleCenter,
                    0,
                    textBackground,
                    new EventArg0(MainGame.Singleton.Exit),
                    this
                    )
                );

            foreach (var child in uiMenu.Children)
                child.SetColor(new Color(28, 28, 28));

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
                ContentHelper.Box4x4_Sprite,
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
