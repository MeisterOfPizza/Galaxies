﻿using Galaxies.Core;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Galaxies.UI.Elements
{

    /// <summary>
    /// Groups UI Elements into one single head element. This can be useful when dealing with text and texture based components that needs to be positioned together.
    /// </summary>
    class UIGroup : UIElement, IContainer
    {

        protected List<UIGroupElement> GroupElements { get; private set; } = new List<UIGroupElement>();

        public override bool Visable
        {
            get
            {
                return visable;
            }

            set
            {
                visable = value;

                //Make every child visable/invisable.
                foreach (UIGroupElement groupElement in GroupElements)
                {
                    groupElement.UIElement.Visable = value;
                }
            }
        }

        #region IContainer

        public IList<UIElement> Children
        {
            get
            {
                return GroupElements.Select(ge => ge.UIElement).ToList();
            }
        }

        #endregion

        public UIGroup(Transform transform, Texture2D sprite, Screen screen) : base(transform, sprite, screen)
        {

        }

        /// <summary>
        /// Adds a new UI Element to the <see cref="GroupElements"/> list with the specified local position set as the <see cref="Core.GameObject.Position"/>.
        /// </summary>
        public T AddUIElement<T>(T uiElement) where T : UIElement
        {
            GroupElements.Add(new UIGroupElement(uiElement, uiElement.Transform.Position));

            //Add the UI Element to the screen's clickable items.
            screen.AddInteractable(uiElement); //Returns false if it wasn't an interactable.
            screen.AddScrollable(uiElement); //Returns false if it wasn't a scrollable.

            return uiElement;
        }

        public void CalculatePositions()
        {
            foreach (UIGroupElement groupElement in GroupElements)
            {
                groupElement.UIElement.Transform.Position = transform.Position + groupElement.GroupPosition;
            }
        }

        #region Overriden methods

        public override void PositionChanged()
        {
            base.PositionChanged();

            CalculatePositions();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (UIGroupElement groupElement in GroupElements)
            {
                groupElement.UIElement.Draw(spriteBatch);
            }
        }

        #endregion

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
