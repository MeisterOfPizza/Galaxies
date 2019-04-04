using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Screens;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    /// <summary>
    /// Large background animation.
    /// </summary>
    class UIBackgroundAnimation : UIElement
    {

        public GIF Animation { get; private set; }

        double gifUpdateInterval;
        double timeSinceLastUpdate;

        Vector2 drawSize;

        public UIBackgroundAnimation(Transform transform, GIF animation, Screen screen) : base(transform, null, screen)
        {
            this.Animation = animation;
            gifUpdateInterval = Animation.GIFSpeed;

            SetOrigin(Animation.Bounds / 2f);

            SetDimensions();
        }

        private void SetDimensions()
        {
            float ratioX = Animation.Bounds.X / GameUIController.WindowWidth;  //Ratio of GIF width / screen width
            float ratioY = Animation.Bounds.Y / GameUIController.WindowHeight; //Ratio of GIF height / screen height

            //We want to cover the screen with the GIF (not exposing the background).
            //This means that some parts of the GIF will be left outside the screen, but that is ok.
            //To get this effect, we divide the default ratio (1) by the smallest ratio (X or Y).
            float scale = 1f / MathHelper.Min(ratioX, ratioY);

            //Then we apply this scaling by multiplying the original animation size with the new scale factor.
            //Then we save the new size for future use.
            drawSize = Animation.Bounds * scale;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visable && Animation != null)
            {
                spriteBatch.Draw(Animation.Current, new Rectangle(transform.RawX, transform.RawY, (int)drawSize.X, (int)drawSize.Y), null, color, transform.Rotation, Origin, SpriteEffects.None, 0f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastUpdate > gifUpdateInterval)
            {
                Animation.Next();

                timeSinceLastUpdate = 0;
            }
        }

    }

}
