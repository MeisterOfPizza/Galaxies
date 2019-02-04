using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    /// <summary>
    /// Creates a list of UI Elements in descending order.
    /// </summary>
    class UIColumn : UIContainer
    {

        /// <summary>
        /// This is the responsive height.
        /// </summary>
        protected int ResponsiveMaxY;

        public UIColumn(Texture2D sprite, Vector2 position, float rotation, Color color, Screen screen, int spaceY, int borderY, bool responsiveSize = true) : base(sprite, position, rotation, color, screen, 0, spaceY, 0, borderY, responsiveSize)
        {
        }

        protected override void CalculatePositions()
        {
            int currentY = BorderY;

            for (int i = 0; i < Container.Count; i++)
            {
                Container[i].Position = Position + new Vector2((Width - Container[i].Width) / 2f, currentY);

                currentY += Container[i].Height + SpaceY;
            }

            ResponsiveMaxY = currentY;
        }

        protected override void CalculateSize()
        {
            SetDrawHeight(ResponsiveMaxY);
        }

    }

}
