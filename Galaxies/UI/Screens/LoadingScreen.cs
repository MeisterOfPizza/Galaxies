using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UIControllers;

namespace Galaxies.UI.Screens
{

    class LoadingScreen : Screen
    {

        public override void CreateUI()
        {
            AddUIElement(new UIText(
                new Transform(Alignment.MiddleCenter, GameUIController.WindowSize),
                SpriteHelper.Arial_Font,
                "Loading...",
                TextAlign.MiddleCenter,
                0,
                this
                ));
        }

    }

}
