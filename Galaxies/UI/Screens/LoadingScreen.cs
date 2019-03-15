using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UIControllers;
using System.Threading.Tasks;

namespace Galaxies.UI.Screens
{

    class LoadingScreen : Screen
    {

        public override Task CreateUIAsync()
        {
            AddUIElement(new UIText(
                new Transform(Alignment.MiddleCenter, GameUIController.Window.ClientBounds.Size.ToVector2()),
                SpriteHelper.Arial_Font,
                "Loading...",
                TextAlign.MiddleCenter,
                0,
                this
                ));

            return null;
        }

    }

}
