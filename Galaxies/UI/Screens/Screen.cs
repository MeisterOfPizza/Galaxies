using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Galaxies.UI.Screens
{

    abstract class Screen
    {

        private const double SELECT_COOLDOWN = 0.15f;

        private List<UIElement> UIElements { get; set; } = new List<UIElement>();

        /// <summary>
        /// Clickable UI Elements. An element is also selectable if it's clickable, and vice versa.
        /// </summary>
        protected List<UIElement> ClickableElements { get; set; } = new List<UIElement>();

        protected int SelectedIndex;

        private double selectionCooldown;

        public abstract void CreateUI(ContentManager content);

        /// <summary>
        /// Look for keystrokes or mouse movement.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            selectionCooldown += gameTime.ElapsedGameTime.TotalSeconds;

            if (selectionCooldown > SELECT_COOLDOWN)
            {
                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.GetPressedKeys().Length > 0)
                {
                    CycleSelected(keyboardState);

                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        ClickSelected();
                    }

                    selectionCooldown = 0;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (UIElement elem in UIElements)
            {
                elem.Draw(spriteBatch);
            }
        }

        private void CycleSelected(KeyboardState keyboardState)
        {
            if (ClickableElements.Count > 0)
            {
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Tab))
                {
                    ClickableElements[SelectedIndex].Deselect();

                    SelectedIndex--;

                    if (SelectedIndex < 0)
                    {
                        SelectedIndex = ClickableElements.Count - 1;
                    }

                    ClickableElements[SelectedIndex].Select();
                }
                else if (keyboardState.IsKeyDown(Keys.Down) || (keyboardState.IsKeyDown(Keys.Tab) && keyboardState.IsKeyDown(Keys.LeftShift)))
                {
                    ClickableElements[SelectedIndex].Deselect();

                    SelectedIndex++;

                    if (SelectedIndex > ClickableElements.Count - 1)
                    {
                        SelectedIndex = 0;
                    }

                    ClickableElements[SelectedIndex].Select();
                }
            }
        }

        protected void ClickSelected()
        {
            if (SelectedIndex >= 0 && SelectedIndex < ClickableElements.Count && ClickableElements.Count > 0)
            {
                ClickableElements[SelectedIndex].Click();
            }
        }

        #region Handling clickables

        /// <summary>
        /// Adds an already existing UI Clickable Element to the <see cref="ClickableElements"/> list.
        /// This is NOT the same as <see cref="AddClickableUIElement(UIElement)"/>!
        /// Use that if you want to add a new UI Element that's clickable.
        /// Mainly called after altering <see cref="UIElement.Visable"/> property.
        /// </summary>
        public void AddUIClickable(UIElement uiElement)
        {
            if (!ClickableElements.Contains(uiElement))
            {
                ClickableElements.Add(uiElement);
            }
        }

        /// <summary>
        /// Removes an already existing UI Clickable Element from the <see cref="ClickableElements"/> list.
        /// Mainly called after altering <see cref="UIElement.Visable"/> property.
        /// </summary>
        public void RemoveUIClickable(UIElement uiElement)
        {
            var index = ClickableElements.IndexOf(uiElement);

            if (index != -1)
            {
                ClickableElements.Remove(uiElement);

                SelectedIndex = 0;
            }
        }

        #endregion

        #region Adding UI Elements

        /// <summary>
        /// Adds a UI Element and makes it selectable if is has <see cref="UIElement.CanBeClicked"/> set to true.
        /// </summary>
        /// <typeparam name="T">UIElement descendant of type T.</typeparam>
        /// <param name="uiElement">The UI Element to add.</param>
        /// <returns>Returns the craeted UI Element.</returns>
        protected T AddUIElement<T>(T uiElement) where T : UIElement
        {
            UIElements.Add(uiElement);

            if (uiElement.CanBeClicked)
            {
                ClickableElements.Add(uiElement);
            }

            //Select the first UI Element if uiElement is the only UI Element.
            if (ClickableElements.Count == 1 && ClickableElements[0] == uiElement)
            {
                ClickableElements[0].Select();
            }

            return uiElement;
        }

        /// <summary>
        /// Adds the UI Element to the <see cref="ClickableElements"/> list.
        /// Mainly used by <see cref="UIContainer"/>.
        /// </summary>
        public void AddClickableUIElement(UIElement uiElement)
        {
            ClickableElements.Add(uiElement);

            //Select the first UI Element if uiElement is the only UI Element.
            if (ClickableElements.Count == 1 && ClickableElements[0] == uiElement)
            {
                ClickableElements[0].Select();
            }
        }

        #endregion

    }

}
