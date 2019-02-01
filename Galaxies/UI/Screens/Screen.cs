using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Galaxies.UI.Screens
{

    abstract class Screen
    {

        private List<UIElement> UIElements { get; set; } = new List<UIElement>();

        protected List<UIElement> ClickableElements { get; set; } = new List<UIElement>();

        protected int SelectedIndex { get; set; }

        public abstract void CreateUI(ContentManager content);

        /// <summary>
        /// Look for keystrokes or mouse movement.
        /// </summary>
        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            CycleSelected(keyboardState);
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
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    SelectedIndex++;

                    if (SelectedIndex > ClickableElements.Count)
                    {
                        SelectedIndex = 0;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    SelectedIndex--;

                    if (SelectedIndex < 0)
                    {
                        SelectedIndex = ClickableElements.Count;
                    }
                }

                ClickableElements[SelectedIndex].Select();
            }
        }

        protected void ClickSelected()
        {
            if (SelectedIndex >= 0 && SelectedIndex < ClickableElements.Count && ClickableElements.Count > 0)
            {
                ClickableElements[SelectedIndex].Click();
            }
        }

        /// <summary>
        /// Adds a UI Element and makes it selectable if is has <see cref="UIElement.CanBeClicked"/> set to true.
        /// </summary>
        /// <param name="uiElement">The UI Element to add.</param>
        /// <returns>Returns the craeted UI Element.</returns>
        protected UIElement AddUIElement(UIElement uiElement)
        {
            UIElements.Add(uiElement);

            if (uiElement.CanBeClicked)
            {
                ClickableElements.Add(uiElement);
            }

            return uiElement;
        }

    }

}
