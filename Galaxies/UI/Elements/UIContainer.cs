﻿using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Galaxies.UI
{

    /// <summary>
    /// Allows the element to have a subset of UI Element children, while also enabling an easy way of selecting them.
    /// Disables the ability to click this element.
    /// </summary>
    abstract class UIContainer : UIElement
    {

        protected List<UIElement> Container { get; private set; } = new List<UIElement>();

        /// <summary>
        /// Raw size, without any padding or spacing applied.
        /// </summary>
        protected Vector2 RawSize { get; private set; }

        /// <summary>
        /// Padding inwards. Top, right, bottom, left.
        /// </summary>
        protected Vector4 Padding { get; private set; }

        /// <summary>
        /// Space between items. X (left and right) and Y (top and bottom).
        /// </summary>
        protected Vector2 Spacing { get; private set; }

        private bool ResponsiveSize { get; set; }

        public UIContainer(Transform transform, Texture2D sprite, Screen screen, Vector4 padding, Vector2 spacing, bool responsiveSize)
            : base(transform, sprite, screen)
        {
            this.RawSize = transform.Size;

            transform.Size = new Vector2(transform.Width + padding.W + padding.Y, transform.Height + padding.X + padding.Z);

            this.Padding = padding;
            this.Spacing = spacing;

            this.ResponsiveSize = responsiveSize;
        }

        protected abstract void CalculatePositions();
        protected abstract void CalculateSize();

        #region Adding UI Elements

        /// <summary>
        /// Called whenever a new UI Element is added to the container.
        /// </summary>
        protected virtual void UIElementAdded(UIElement addedElement)
        {
            //Do nothing here.
            //This method will not be utilized by every child, therefore, keep it overridable.
        }

        /// <summary>
        /// Adds a new UI Element to the container.
        /// </summary>
        public T AddUIElement<T>(T uiElement) where T : UIElement
        {
            Container.Add(uiElement);

            //Add the UI Element to the screen's clickable items.
            screen.AddInteractable(uiElement); //Returns false if it wasn't an interactable.

            CalculatePositions();

            if (ResponsiveSize)
            {
                CalculateSize();
            }

            UIElementAdded(uiElement);

            return uiElement;
        }

        /// <summary>
        /// Adds multiple new UI Elements to the container.
        /// </summary>
        public void AddUIElements(params UIElement[] uiElements)
        {
            for (int i = 0; i < uiElements.Length; i++)
            {
                Container.Add(uiElements[i]);

                //Add the UI Element to the screen's clickable items.
                screen.AddInteractable(uiElements[i]); //Returns false if it wasn't an interactable.

                UIElementAdded(uiElements[i]);
            }

            CalculatePositions();

            if (ResponsiveSize)
            {
                CalculateSize();
            }
        }

        #endregion

        #region Removing UI Elements

        /// <summary>
        /// Called whenever a UI Element is removed from the container.
        /// </summary>
        /// <param name="removedElement"></param>
        protected virtual void UIElementRemoved(UIElement removedElement, int removedIndex)
        {
            //Do nothing here.
            //This method will not be utilized by every child, therefore, keep it overridable.
        }

        /// <summary>
        /// Removes the UI Element from the container and repositions all existing elements.
        /// </summary>
        public bool RemoveUIElement(UIElement uiElement)
        {
            int  index = Container.IndexOf(uiElement);
            bool value = Container.Remove(uiElement);

            screen.RemoveUIElement(uiElement);

            //Don't call the method unless the item existed.
            if (value)
            {
                UIElementRemoved(uiElement, index);
            }

            return value;
        }

        #endregion

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (UIElement element in Container)
            {
                element.Draw(spriteBatch);
            }
        }

        #region Helpers

        /// <summary>
        /// Hides all the elements inside the container.
        /// </summary>
        protected void HideAll()
        {
            for (int i = 0; i < Container.Count; i++)
            {
                Container[i].Visable = false;
            }
        }

        #endregion

    }

}
