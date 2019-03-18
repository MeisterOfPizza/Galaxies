using Galaxies.Core;
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
        protected int responsiveMaxY;

        public UIColumn(Transform transform, Texture2D sprite, Screen screen, Vector4 padding, Vector2 spacing, bool responsiveSize = true) : base(transform, sprite, screen, padding, spacing, responsiveSize)
        {

        }

        protected override void CalculatePositions()
        {
            int currentY = (int)Padding.X;
            float topY = -transform.Height / 2f;

            for (int i = 0; i < Container.Count; i++)
            {
                Container[i].Transform.Position = transform.Position + new Vector2(0, topY + currentY + Container[i].Transform.Height / 2f);

                currentY += Container[i].Transform.RawHeight + (int)Spacing.Y;
            }

            responsiveMaxY = currentY + (int)Padding.Z;
        }

        protected override void CalculateSize()
        {
            transform.SetSizeY(responsiveMaxY);
        }

    }

}
