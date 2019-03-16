using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Screens;
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

        private double GIFUpdateInterval   { get; set; }
        private double TimeSinceLastUpdate { get; set; }

        public UIBackgroundAnimation(Transform transform, GIF animation, Screen screen) : base(transform, null, screen)
        {
            this.Animation = animation;
            Animation.Reset();
            GIFUpdateInterval = Animation.GIFSpeed;

            SetOrigin(Animation.Bounds / 2f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visable && Animation != null)
            {
                spriteBatch.Draw(Animation.Current, new Rectangle(transform.RawX, transform.RawY, transform.RawWidth, transform.RawHeight), null, color, transform.Rotation, Origin, SpriteEffects.None, 0f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            TimeSinceLastUpdate += gameTime.ElapsedGameTime.TotalSeconds;

            if (TimeSinceLastUpdate > GIFUpdateInterval)
            {
                Animation.Next();

                TimeSinceLastUpdate = 0;
            }
        }

    }

}
