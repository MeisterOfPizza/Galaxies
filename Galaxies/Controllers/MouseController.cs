using Galaxies.UI;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Galaxies.Controllers
{

    static class MouseController
    {

        public static Screen CurrentScreen { get; set; }

        private static Point LastPosition         { get; set; }
        private static bool  HasReleasedButton    { get; set; } = true;
        private static int   LastScrollWheelValue { get; set; }

        private static IInteractable ElementUnderMouse { get; set; }

        public static void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed && HasReleasedButton)
            {
                ClickEvent();

                HasReleasedButton = false;
            }

            if (state.LeftButton == ButtonState.Released)
            {
                HasReleasedButton = true;
            }

            if (LastPosition != state.Position)
            {
                MovementEvent();
            }

            LastPosition = state.Position;

            if (state.ScrollWheelValue != LastScrollWheelValue)
            {
                int scrollValue = MathHelper.Clamp(state.ScrollWheelValue - LastScrollWheelValue, -1, 1);

                ScrollEvent(scrollValue);
            }

            LastScrollWheelValue = state.ScrollWheelValue;
        }

        private static void ClickEvent()
        {
            if (ElementUnderMouse != null && ElementUnderMouse.IsInteractable)
            {
                ElementUnderMouse.Click();
            }
        }

        private static void MovementEvent()
        {
            Vector2 mousePos = LastPosition.ToVector2();

            if (ElementUnderMouse != null && ElementUnderMouse.IsInteractable)
            {
                if (!((UIElement)ElementUnderMouse).Contains(mousePos))
                {
                    ElementUnderMouse.Deselect();

                    ElementUnderMouse = null;
                }
            }

            //Go through all the interactables, from the top one to the bottom one ("layers", z-index).
            //WARNING: Might procude an error, as selecting and deselecting can remove interactables and cause the count to go down?
            for (int i = CurrentScreen.Interactables.Count - 1; i >= 0; i--)
            {
                if (CurrentScreen.Interactables[i].IsInteractable && ((UIElement)CurrentScreen.Interactables[i]).Visable && ((UIElement)CurrentScreen.Interactables[i]).Contains(mousePos))
                {
                    if (ElementUnderMouse != CurrentScreen.Interactables[i])
                    {
                        if (ElementUnderMouse != null)
                        {
                            ElementUnderMouse.MouseExit(); //Deselect the old one.
                        }

                        ElementUnderMouse = CurrentScreen.Interactables[i];
                        ElementUnderMouse.MouseEnter(); //Select the new one.
                    }

                    return; //We only want to select the one at the top.
                }
            }
        }

        private static void ScrollEvent(int value)
        {
            Vector2 mousePos = LastPosition.ToVector2();

            //WARNING: Might procude an error, as scrolling can remove scrollables and cause the count to go down?
            for (int i = 0; i < CurrentScreen.Scrollables.Count; i++)
            {
                if (CurrentScreen.Scrollables[i].IsScrollable && CurrentScreen.UIElements[i].Contains(mousePos))
                {
                    CurrentScreen.Scrollables[i].MouseScroll(value);
                }
            }
        }

    }

}
