using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Screens
{

    class VictoryScreen : Screen
    {

        public override void CreateUI()
        {
            AddUIElement(new UIBackgroundAnimation(
                new Transform(Alignment.MiddleCenter, GameUIController.WindowSize),
                ContentHelper.Space_Background_Animation_2,
                this
                ));

            AddUIElement(new UIText(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(0, -250), new Vector2(250, 50)), new Vector2(250, 50)),
                ContentHelper.Arial_Font,
                "Victory!",
                TextAlign.MiddleCenter,
                0,
                this
                ));

            AddUIElement(new UIButton(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(-60, 0), new Vector2(100, 50)), new Vector2(100, 50)),
                ContentHelper.Arial_Font,
                "Menu",
                TextAlign.MiddleCenter,
                0,
                ContentHelper.Box4x4_Sprite,
                new EventArg1<EventArg>(GameUIController.CreateMenuScreen, null),
                this
                )).SetColor(new Color(28, 28, 28));

            AddUIElement(new UIButton(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(60, 0), new Vector2(100, 50)), new Vector2(100, 50)),
                ContentHelper.Arial_Font,
                "Exit",
                TextAlign.MiddleCenter,
                0,
                ContentHelper.Box4x4_Sprite,
                new EventArg0(MainGame.Singleton.Exit),
                this
                )).SetColor(new Color(28, 28, 28));
        }

    }

}
