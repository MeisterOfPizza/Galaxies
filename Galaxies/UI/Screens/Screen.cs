using Galaxies.Core;
using Galaxies.UI.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Galaxies.UI.Screens
{

    abstract class Screen
    {

        #region Constants

        /// <summary>
        /// Delay between switches of interactables with the keyboard.
        /// </summary>
        private const double KB_SELECT_COOLDOWN = 0.15f;

        #endregion

        #region Containers

        /// <summary>
        /// All UI Elements on the screen.
        /// </summary>
        public List<UIElement> UIElements { get; protected set; } = new List<UIElement>();

        /// <summary>
        /// All UI Elements that are interactable and currently on the screen.
        /// </summary>
        public List<IInteractable> Interactables { get; protected set; } = new List<IInteractable>();

        /// <summary>
        /// All UI Elements that are scrollable and currently on the screen.
        /// </summary>
        public List<IScrollable> Scrollables { get; protected set; } = new List<IScrollable>();

        #endregion

        #region Focus

        /// <summary>
        /// The focused interactable.
        /// </summary>
        private IInteractable ForceFocusedInteractable { get; set; }

        /// <summary>
        /// Is the focus forced?
        /// </summary>
        private bool ForceFocusOn { get; set; }

        #endregion

        #region Keyboard selection

        /// <summary>
        /// Selected interactable made with the keyboard.
        /// </summary>
        protected IInteractable kb_selectedInteractable { get; private set; }

        /// <summary>
        /// How long since the last selection with the keyboard was made.
        /// </summary>
        protected double kb_selectionCooldown { get; private set; }

        #endregion

        #region Mouse selection

        /// <summary>
        /// Selected interactable made with the mouse (interactable beneath the cursor).
        /// </summary>
        protected IInteractable ms_selectedInteractable { get; private set; }

        /// <summary>
        /// Latest position of the cursor (last frame).
        /// </summary>
        protected Point ms_lastPosition { get; private set; }

        /// <summary>
        /// Has the user released the [left] mouse button?
        /// </summary>
        protected bool ms_hasReleasedButton { get; private set; } = true;

        /// <summary>
        /// Scroll wheel value.
        /// </summary>
        protected int ms_scrollWheelValue { get; private set; }

        #endregion

        /// <summary>
        /// Events called wheneever a new selection with the keyboard was made.
        /// </summary>
        public EventArg1<UIElement> kb_selectCallbacks { get; private set; }

        public Screen()
        {
            kb_selectCallbacks = new EventArg1<UIElement>();
        }

        public abstract void CreateUI();

        #region Screen updates

        /// <summary>
        /// Look for keystrokes or mouse movement.
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            KeyboardUpdate(gameTime);
            MouseUpdate();

            UpdateUIElements(gameTime);
        }

        private void UpdateUIElements(GameTime gameTime)
        {
            foreach (var element in UIElements)
            {
                element.Update(gameTime);
            }
        }

        private void KeyboardUpdate(GameTime gameTime)
        {
            kb_selectionCooldown += gameTime.ElapsedGameTime.TotalSeconds;

            if (kb_selectionCooldown > KB_SELECT_COOLDOWN)
            {
                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.GetPressedKeys().Length > 0)
                {
                    KeyboardSelection(keyboardState);

                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        KeyboardClickEvent();
                    }

                    kb_selectionCooldown = 0;
                }
            }
        }

        private void MouseUpdate()
        {
            MouseState state = Mouse.GetState();

            //Check if the user has scrolled since last updates:
            if (state.ScrollWheelValue != ms_scrollWheelValue)
            {
                int scrollValue = MathHelper.Clamp(state.ScrollWheelValue - ms_scrollWheelValue, -1, 1);

                MouseScrollWheelEvent(scrollValue);
            }

            //Update the scroll wheel value:
            ms_scrollWheelValue = state.ScrollWheelValue;

            //Check if the user has moved the mouse since last update:
            if (ms_lastPosition != state.Position)
            {
                MouseMovementEvent();
            }

            //Update the mouse position:
            ms_lastPosition = state.Position;

            //Check if the user has clicked the left mouse button AND has released it since last click:
            if (state.LeftButton == ButtonState.Pressed && ms_hasReleasedButton)
            {
                MouseClickEvent();

                //We don't know if they've released it yet:
                ms_hasReleasedButton = false;
            }

            //Check if the user has released the left mouse button:
            if (state.LeftButton == ButtonState.Released)
            {
                //They have, reset it so they can click the left mouse button again:
                ms_hasReleasedButton = true;
            }
        }

        #endregion

        #region Keyboard selection

        private void KeyboardSelection(KeyboardState keyboardState)
        {
            if (Interactables.Count > 0)
            {
                //If no interactable has been selected, select the first one:
                if (kb_selectedInteractable == null && keyboardState.GetPressedKeys().Length > 0)
                {
                    kb_selectedInteractable = Interactables[0];
                    kb_selectedInteractable.Select();

                    return;
                }

                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Tab))
                {
                    int nextIndex = Interactables.IndexOf(kb_selectedInteractable) - 1;

                    if (nextIndex < 0)
                    {
                        nextIndex = Interactables.Count - 1;
                    }

                    //Only deselect the current interactable if it's not currently selected by the mouse.
                    if (kb_selectedInteractable != ms_selectedInteractable)
                    {
                        kb_selectedInteractable.Deselect();
                    }

                    kb_selectedInteractable = Interactables[nextIndex];
                    kb_selectedInteractable.Select();
                }
                else if (keyboardState.IsKeyDown(Keys.Down) || (keyboardState.IsKeyDown(Keys.Tab) && keyboardState.IsKeyDown(Keys.LeftShift)))
                {
                    int nextIndex = Interactables.IndexOf(kb_selectedInteractable) + 1;

                    if (nextIndex >= Interactables.Count)
                    {
                        nextIndex = 0;
                    }

                    //Only deselect the current interactable if it's not currently selected by the mouse.
                    if (kb_selectedInteractable != ms_selectedInteractable)
                    {
                        kb_selectedInteractable.Deselect();
                    }

                    kb_selectedInteractable = Interactables[nextIndex];
                    kb_selectedInteractable.Select();
                }

                //Keyboard selection callback:
                if (kb_selectCallbacks != null)
                {
                    kb_selectCallbacks.SetArguments(kb_selectedInteractable);
                    kb_selectCallbacks.Invoke();
                }
            }
        }

        private void KeyboardClickEvent()
        {
            if (kb_selectedInteractable != null && kb_selectedInteractable.IsInteractable)
            {
                if (!ForceFocusOn)
                {
                    kb_selectedInteractable.Click();
                }
                else if (ForceFocusOn && ForceFocusedInteractable == kb_selectedInteractable)
                {
                    kb_selectedInteractable.Click();
                }
            }
        }

        #endregion

        #region Mouse selection

        /// <summary>
        /// Mouse scroll event
        /// </summary>
        private void MouseScrollWheelEvent(int value)
        {
            Vector2 mousePos = ms_lastPosition.ToVector2();

            //WARNING: Might procude an error, as scrolling can remove scrollables and cause the count to go down?
            for (int i = Scrollables.Count - 1; i >= 0; i--)
            {
                if (Scrollables[i].IsScrollable)
                {
                    GameObject scrollable = (GameObject)Scrollables[i];

                    if (scrollable.Visable && scrollable.Contains(mousePos))
                    {
                        Scrollables[i].MouseScroll(value);

                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Mouse movement event
        /// </summary>
        private void MouseMovementEvent()
        {
            Vector2 mousePos = ms_lastPosition.ToVector2();

            //Deselect the current interactable beneath the mouse if the mouse is no longer inside the interactable.
            if (ms_selectedInteractable != null && ms_selectedInteractable.IsInteractable)
            {
                if (!((GameObject)ms_selectedInteractable).Contains(mousePos) && ms_selectedInteractable != kb_selectedInteractable)
                {
                    ms_selectedInteractable.MouseExit();

                    ms_selectedInteractable = null;
                }
            }

            //Go through all the interactables, from the top one to the bottom one ("layers", z-index).
            //WARNING: Might procude an error, as selecting and deselecting can remove interactables and cause the count to go down?
            for (int i = Interactables.Count - 1; i >= 0; i--)
            {
                if (Interactables[i].IsInteractable)
                {
                    GameObject interactable = (GameObject)Interactables[i];

                    if (interactable.Visable && interactable.Contains(mousePos))
                    {
                        if (ms_selectedInteractable != Interactables[i])
                        {
                            ms_selectedInteractable = Interactables[i];
                            ms_selectedInteractable.MouseEnter(); //Select the new one.
                        }

                        return; //We only want to select the one at the top.
                    }
                }
            }
        }

        /// <summary>
        /// Mouse click event
        /// </summary>
        private void MouseClickEvent()
        {
            if (ms_selectedInteractable != null && ms_selectedInteractable.IsInteractable)
            {
                if (!ForceFocusOn)
                {
                    ms_selectedInteractable.Click();
                }
                else if (ForceFocusOn && ForceFocusedInteractable == ms_selectedInteractable)
                {
                    ms_selectedInteractable.Click();
                }
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
            }

            if (uiElement is IScrollable scrollable)
            {
                Scrollables.Add(scrollable);
            }

            return uiElement;
        }

        /// <summary>
        /// Tries to add the UI Element as an interactable. Returns true if it succeeded, false if it didn't.
        /// </summary>
        public bool AddInteractable(UIElement uiElement)
        {
            if (uiElement is IInteractable interactable)
            {
                Interactables.Add(interactable);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to add the UI Element as a scrollable. Returns true if it succeeded, false if it didn't.
        /// </summary>
        public bool AddScrollable(UIElement uiElement)
        {
            if (uiElement is IScrollable scrollable)
            {
                Scrollables.Add(scrollable);

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
                Interactables.Remove(interactable);

                //Deactivate forced focus:
                if (ForceFocusedInteractable == interactable)
                {
                    ForceFocusOn = false;
                    ForceFocusedInteractable = null;
                }

                //Unselect it from the "keyboard":
                if (kb_selectedInteractable == interactable)
                {
                    kb_selectedInteractable = null;
                }

                //Unselect it from the "mouse":
                if (ms_selectedInteractable == interactable)
                {
                    ms_selectedInteractable = null;
                }
            }

            if (uiElement is IScrollable scrollable)
            {
                Scrollables.Remove(scrollable);
            }

            if (uiElement is IContainer container)
            {
                foreach (UIElement child in container.Children)
                {
                    RemoveUIElement(child);
                }
            }

            uiElement.OnDestroy();
        }

        #endregion

        #region Focus

        /// <summary>
        /// Only allows the user the interact with the given interactable.
        /// </summary>
        public void ForceFocus(IInteractable interactable)
        {
            ForceFocusedInteractable = interactable;

            ForceFocusOn = true;
        }

        #endregion

        #region Drawing

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

        #endregion

        #region Screen

        /// <summary>
        /// Call this whenever this screen becomes obsolete.
        /// </summary>
        public void DestroyScreen()
        {
            foreach (UIElement element in UIElements)
            {
                element.OnDestroy();
            }
        }

        #endregion

    }

}
