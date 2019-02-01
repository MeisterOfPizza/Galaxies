﻿using Galaxies.UI.Screens;
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

        protected int SpaceX  { get; set; }
        protected int SpaceY  { get; set; }
        protected int BorderX { get; set; }
        protected int BorderY { get; set; }

        private bool ResponsiveSize { get; set; }

        public UIContainer(Texture2D sprite, Vector2 position, float rotation, Color color, OnClickEvent onClick, Screen screen, int spaceX, int spaceY, int borderX, int borderY, bool responsiveSize) : base(sprite, position, rotation, color, onClick, screen, false)
        {
            this.SpaceX  = spaceX;
            this.SpaceY  = spaceY;
            this.BorderX = borderX;
            this.BorderY = borderY;

            this.ResponsiveSize = responsiveSize;
        }

        protected abstract void CalculatePositions();
        protected abstract void CalculateSize();

        /// <summary>
        /// Adds a new UI Element to the container.
        /// </summary>
        public T AddUIElement<T>(T uiElement) where T : UIElement
        {
            Container.Add(uiElement);

            if (uiElement.CanBeClicked)
            {
                //Add the UI Element to the screen's clickable items.
                Screen.AddClickableUIElement(uiElement);
            }

            CalculatePositions();

            if (ResponsiveSize)
            {
                CalculateSize();
            }

            return uiElement;
        }

        /// <summary>
        /// Adds multiple new UI Elements to the container and sets their draw width and draw height equal to <paramref name="drawWidth"/> and <paramref name="drawHeight"/>.
        /// </summary>
        public void AddUIElements(int drawWidth, int drawHeight, params UIElement[] uiElements)
        {
            for (int i = 0; i < uiElements.Length; i++)
            {
                Container.Add(uiElements[i]);

                if (uiElements[i].CanBeClicked)
                {
                    //Add the UI Element to the screen's clickable items.
                    Screen.AddClickableUIElement(uiElements[i]);
                }

                uiElements[i].SetDrawSize(drawWidth, drawHeight);
            }

            CalculatePositions();

            if (ResponsiveSize)
            {
                CalculateSize();
            }
        }

        public override void Select()
        {
            //Do nothing
        }

        public override void Deselect()
        {
            //Do nothing
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (UIElement element in Container)
            {
                element.Draw(spriteBatch);
            }
        }

    }

}