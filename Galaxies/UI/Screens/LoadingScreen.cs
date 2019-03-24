using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Screens
{

    class LoadingScreen : Screen
    {

        UIText    gameTipsText;
        UIElement loadingSpinner;

        double timeSinceLastGameTip;

        public override void CreateUI()
        {
            AddUIElement(new UIElement(
                new Transform(Alignment.MiddleCenter, GameUIController.WindowSize),
                ContentHelper.GetSprite("Sprites/Backgrounds/Static/space-background-4"),
                this
                ));

            loadingSpinner = AddUIElement(new UIElement(
                new Transform(Transform.CreatePosition(Alignment.BottomRight, new Vector2(-25), new Vector2(75)), new Vector2(75)),
                ContentHelper.GetSprite("Sprites/Effects/Dual Ring"),
                this
                ));

            gameTipsText = AddUIElement(new UIText(
                new Transform(Transform.CreatePosition(Alignment.BottomLeft, new Vector2(25, -25), new Vector2(GameUIController.WindowWidth / 2f, 100)), new Vector2(GameUIController.WindowWidth / 2f, 100)),
                ContentHelper.Arial_Font,
                GameTipsController.RandomTip(),
                TextAlign.BottomLeft,
                0,
                this
                ));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            loadingSpinner.Transform.Rotation += 0.03f;

            timeSinceLastGameTip += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastGameTip > 10)
            {
                timeSinceLastGameTip = 0;

                gameTipsText.Text = GameTipsController.RandomTip();
            }
        }

    }

}
