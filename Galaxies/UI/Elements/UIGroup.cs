using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Galaxies.UI.Elements
{

    class UIGroup : UIElement
    {

        protected List<UIGroupElement> GroupElements { get; private set; } = new List<UIGroupElement>();

        public UIGroup(Texture2D sprite, Vector2 position, float rotation, Color color, OnClickEvent onClick, Screen screen, bool canBeClicked = true) : base(sprite, position, rotation, color, onClick, screen, canBeClicked)
        {

        }

        /// <summary>
        /// Adds a new UI Element to the <see cref="GroupElements"/> list with the specified local position set as the <see cref="Core.GameObject.Position"/>.
        /// </summary>
        public T AddUIElement<T>(T uiElement) where T : UIElement
        {
            GroupElements.Add(new UIGroupElement(uiElement, uiElement.Position));

            if (uiElement.CanBeClicked)
            {
                //Add the UI Element to the screen's clickable items.
                Screen.AddClickableUIElement(uiElement);
            }

            return uiElement;
        }

        protected override void PositionChanged()
        {
            base.PositionChanged();

            CalculatePositions();
        }

        public void CalculatePositions()
        {
            foreach (UIGroupElement groupElement in GroupElements)
            {
                groupElement.UIElement.Position = Position + groupElement.GroupPosition;
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
