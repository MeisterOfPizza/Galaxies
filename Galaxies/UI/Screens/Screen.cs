using Galaxies.Core;
using Galaxies.UI.Interfaces;
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

        public List<UIElement> UIElements { get; protected set; } = new List<UIElement>();

        public List<IInteractable> Interactables { get; protected set; } = new List<IInteractable>();
        public List<IScrollable>   Scrollables   { get; protected set; } = new List<IScrollable>();

        protected int selectedIndex;

        private double selectionCooldown;

        public EventArg1<UIElement> SelectCallbacks { get; set; }

        public Screen()
        {
            SelectCallbacks = new EventArg1<UIElement>();
        }

        public abstract void CreateUI(ContentManager content);

        /// <summary>
        /// Look for keystrokes or mouse movement.
        /// </summary>
        public virtual void Update(GameTime gameTime)
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
                if (elem.Visable)
                {
                    elem.Draw(spriteBatch);
                }
            }
        }

        private void CycleSelected(KeyboardState keyboardState)
        {
            if (Interactables.Count > 0)
            {
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Tab))
                {
                    Interactables[selectedIndex].Deselect();

                    selectedIndex--;

                    if (selectedIndex < 0)
                    {
                        selectedIndex = Interactables.Count - 1;
                    }

                    Interactables[selectedIndex].Select();

                    if (SelectCallbacks != null)
                    {
                        SelectCallbacks.SetArguments(Interactables[selectedIndex]);
                        SelectCallbacks.Invoke();
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.Down) || (keyboardState.IsKeyDown(Keys.Tab) && keyboardState.IsKeyDown(Keys.LeftShift)))
                {
                    Interactables[selectedIndex].Deselect();

                    selectedIndex++;

                    if (selectedIndex > Interactables.Count - 1)
                    {
                        selectedIndex = 0;
                    }

                    Interactables[selectedIndex].Select();

                    if (SelectCallbacks != null)
                    {
                        SelectCallbacks.SetArguments(Interactables[selectedIndex]);
                        SelectCallbacks.Invoke();
                    }
                }
            }
        }

        /// <summary>
        /// The already selected Interactable was unselected, either because it got removed or because it can't be clicked anymore.
        /// Eitherway, reselect the next item.
        /// </summary>
        private void ReselectInteractable()
        {
            if (Interactables.Count > 0)
            {
                selectedIndex = MathHelper.Clamp(selectedIndex, 0, Interactables.Count - 1);

                Interactables[selectedIndex].Select();
            }
        }

        protected void ClickSelected()
        {
            if (selectedIndex >= 0 && selectedIndex < Interactables.Count && Interactables.Count > 0)
            {
                Interactables[selectedIndex].Click();
            }
        }

        /// <summary>
        /// Select the last (or most recent added) Interactable.
        /// </summary>
        public void SelectLast()
        {
            if (Interactables.Count > 0)
            {
                Interactables[selectedIndex].Deselect();
                selectedIndex = Interactables.Count - 1;
                Interactables[selectedIndex].Select();
            }
        }

        public void Select(int index)
        {
            if (index >= 0 && index < Interactables.Count)
            {
                selectedIndex = index;
            }
        }

        #region Managing interactables

        /// <summary>
        /// Removes an already existing Interactable from the <see cref="Interactables"/> list.
        /// Often called after altering <see cref="GameObject.Visable"/> property.
        /// </summary>
        public void RemoveInteractable(IInteractable interactable)
        {
            var index = Interactables.IndexOf(interactable);

            if (index != -1)
            {
                Interactables.Remove(interactable);

                ReselectInteractable();
            }
        }

        #endregion

        #region Adding UI Elements

        /// <summary>
        /// Adds a UI Element and makes it interactable if it implements <see cref="IInteractable"/> and scrollable if it implements <see cref="IScrollable"/>.
        /// </summary>
        /// <typeparam name="T">UIElement descendant of type T.</typeparam>
        /// <param name="uiElement">The UI Element to add.</param>
        /// <returns>Returns the craeted UI Element.</returns>
        public T AddUIElement<T>(T uiElement) where T : UIElement
        {
            UIElements.Add(uiElement);

            if (uiElement is IInteractable interactable)
            {
                Interactables.Add(interactable);

                //Select the first Interactable if interactable is the only UI Element.
                if (Interactables.Count == 1 && Interactables[0] == uiElement)
                {
                    Interactables[0].Select();
                }
            }

            if (uiElement is IScrollable scrollable)
            {
                Scrollables.Add(scrollable);
            }

            return uiElement;
        }

        /// <summary>
        /// Adds the Interactable to the <see cref="Interactables"/> list.
        /// Mainly used by <see cref="UIContainer"/>.
        /// </summary>
        public void AddInteractable(IInteractable interactable)
        {
            Interactables.Add(interactable);

            //Select the first Interactable if interactable is the only UI Element.
            if (Interactables.Count == 1 && Interactables[0] == interactable)
            {
                Interactables[0].Select();
            }
        }

        public bool AddInteractable(UIElement uiElement)
        {
            if (uiElement is IInteractable interactable)
            {
                AddInteractable(interactable);

                return true;
            }

            return false;
        }

        #endregion

        #region Removing UI Elements

        /// <summary>
        /// Removes the UI Element.
        /// </summary>
        public void RemoveUIElement(UIElement uiElement)
        {
            UIElements.Remove(uiElement);

            if (uiElement is IInteractable interactable)
            {
                RemoveInteractable(interactable);
            }

            if (uiElement is IScrollable scrollable)
            {
                Scrollables.Remove(scrollable);
            }
        }

        #endregion

    }

}
