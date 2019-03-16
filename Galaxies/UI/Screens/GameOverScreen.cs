using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Special;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Galaxies.UI.Screens
{

    class GameOverScreen : Screen
    {

        List<UIElement> mainElements;
        UISaveFiles uiLoad;

        public override void CreateUI()
        {
            mainElements = new List<UIElement>();

            mainElements.Add(AddUIElement(new UIText(
                new Transform(Transform.SetPosition(Alignment.MiddleCenter, new Vector2(0, -250), new Vector2(250, 50)), new Vector2(250, 50)),
                SpriteHelper.Arial_Font,
                "Game Over",
                TextAlign.MiddleCenter,
                0,
                this
                )));

            mainElements.Add(AddUIElement(new UIButton(
                new Transform(Transform.SetPosition(Alignment.MiddleCenter, new Vector2(-60, 0), new Vector2(100, 50)), new Vector2(100, 50)),
                SpriteHelper.Arial_Font,
                "Load",
                TextAlign.MiddleCenter,
                0,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(ToggleUILoad),
                this
                )));

            mainElements.Add(AddUIElement(new UIButton(
                new Transform(Transform.SetPosition(Alignment.MiddleCenter, new Vector2(60, 0), new Vector2(100, 50)), new Vector2(100, 50)),
                SpriteHelper.Arial_Font,
                "Exit",
                TextAlign.MiddleCenter,
                0,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(MainGame.Singleton.Exit),
                this
                )));

            uiLoad = AddUIElement(new UISaveFiles(
                new Transform(Alignment.MiddleCenter, new Vector2(250, 600)),
                this,
                false,
                "Load",
                new EventArg0(ToggleUILoad)
                ));

            uiLoad.Visable = false;
        }

        public void ToggleUILoad()
        {
            foreach (var elem in mainElements)
            {
                elem.Visable = uiLoad.Visable;
            }

            uiLoad.Visable = !uiLoad.Visable;
        }

    }

}
