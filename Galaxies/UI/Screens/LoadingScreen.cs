using Galaxies.Core;
using Galaxies.Extensions;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Screens
{

    class LoadingScreen : Screen
    {

        UIElement loadingSpinner;

        public override void CreateUI()
        {
            loadingSpinner = AddUIElement(new UIElement(
                new Transform(Alignment.MiddleCenter, new Vector2(150)),
                SpriteHelper.GetSprite("Sprites/Effects/Dual Ring"),
                this
                ));

            loadingSpinner.SetColor(new Color(255, 74, 14));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            loadingSpinner.Transform.Rotation += 0.03f;
        }

    }

}
