using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    /// <summary>
    /// Creates a list of UI Elements in descending order.
    /// This column does NOT move, and will always push elements down unlike <see cref="UIColumn"/> which will always try to center its children.
    /// </summary>
    class UIFixedColumn : UIContainer
    {

        /// <summary>
        /// This is the responsive height.
        /// </summary>
        protected int responsiveMaxY;

        /// <summary>
        /// This is the original size, without any responsive values added.
        /// </summary>
        private Vector2 originalSize;

        public UIFixedColumn(Transform transform, Texture2D sprite, Screen screen, Vector4 padding, Vector2 spacing, bool responsiveSize = true) : base(transform, sprite, screen, padding, spacing, responsiveSize)
        {
            originalSize = transform.Size;
        }

        protected override void UIElementAdded(UIElement addedElement)
        {
            CalculatePositions();
        }

        protected override void UIElementRemoved(UIElement removedElement, int removedIndex)
        {
            CalculatePositions();
        }

        protected override void CalculatePositions()
        {
            int currentY = (int)Padding.X;
            float topY = -originalSize.Y / 2f;

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
