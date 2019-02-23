using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Galaxies.UI.Elements
{

    /// <summary>
    /// Groups UI Elements into one single head element. This can be useful when dealing with text and texture based components that needs to be positioned together.
    /// </summary>
    class UIGroup : UIElement
    {

        protected List<UIGroupElement> GroupElements { get; private set; } = new List<UIGroupElement>();

        public UIGroup(Transform transform, Texture2D sprite, EventArg onClick, Screen screen, bool canBeClicked = true) : base(transform, sprite, onClick, screen, canBeClicked)
        {

        }

        /// <summary>
        /// Adds a new UI Element to the <see cref="GroupElements"/> list with the specified local position set as the <see cref="Core.GameObject.Position"/>.
        /// </summary>
        public T AddUIElement<T>(T uiElement) where T : UIElement
        {
            GroupElements.Add(new UIGroupElement(uiElement, uiElement.Transform.Position));

            if (uiElement.CanBeClicked)
            {
                //Add the UI Element to the screen's clickable items.
                screen.AddClickableUIElement(uiElement);
            }

            return uiElement;
        }

        public override void PositionChanged()
        {
            base.PositionChanged();

            CalculatePositions();
        }

        public void CalculatePositions()
        {
            foreach (UIGroupElement groupElement in GroupElements)
            {
                groupElement.UIElement.Transform.Position = transform.Position + groupElement.GroupPosition;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (UIGroupElement groupElement in GroupElements)
            {
                groupElement.UIElement.Draw(spriteBatch);
            }
        }

        protected class UIGroupElement
        {

            public UIElement UIElement     { get; set; }
            public Vector2   GroupPosition { get; set; }

            public UIGroupElement(UIElement uiElement, Vector2 groupPosition)
            {
                this.UIElement     = uiElement;
                this.GroupPosition = groupPosition;
            }

        }

    }

}
