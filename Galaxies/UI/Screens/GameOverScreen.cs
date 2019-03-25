using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Special;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Screens
{

    class GameOverScreen : Screen
    {

        UIGroup     mainElements;
        UISaveFiles uiLoad;

        public override void CreateUI()
        {
            AddUIElement(new UIBackgroundAnimation(
                new Transform(Alignment.MiddleCenter, GameUIController.WindowSize),
                ContentHelper.Space_Background_Animation_1,
                this
                ));

            mainElements = AddUIElement(new UIGroup(
                new Transform(Alignment.MiddleCenter, new Vector2(500)),
                null,
                this
                ));

            mainElements.AddUIElement(new UIText(
                new Transform(new Vector2(0, -250), new Vector2(250, 50)),
                ContentHelper.Arial_Font,
                "Game Over",
                TextAlign.MiddleCenter,
                0,
                this
                ));

            mainElements.AddUIElement(new UIButton(
                new Transform(new Vector2(-60, 0), new Vector2(100, 50)),
                ContentHelper.Arial_Font,
                "Load",
                TextAlign.MiddleCenter,
                0,
                ContentHelper.Box4x4_Sprite,
                new EventArg0(ToggleUILoad),
                this
                )).SetColor(new Color(28, 28, 28));

            mainElements.AddUIElement(new UIButton(
                new Transform(new Vector2(60, 0), new Vector2(100, 50)),
                ContentHelper.Arial_Font,
                "Exit",
                TextAlign.MiddleCenter,
                0,
                ContentHelper.Box4x4_Sprite,
                new EventArg0(MainGame.Singleton.Exit),
                this
                )).SetColor(new Color(28, 28, 28));

            uiLoad = AddUIElement(new UISaveFiles(
                new Transform(Alignment.MiddleCenter, new Vector2(250, 600)),
                this,
                false,
                "Load",
                new EventArg0(ToggleUILoad)
                ));

            uiLoad.Visable = false;
        }

        private void ToggleUILoad()
        {
            foreach (var elem in mainElements.Children)
            {
                elem.Visable = uiLoad.Visable;
            }

            uiLoad.Visable = !uiLoad.Visable;
        }

    }

}
