using Galaxies.Core;
using Galaxies.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Interfaces
{

    /// <summary>
    /// Marks this UI Element as an interactable.
    /// </summary>
    interface IInteractable : ICollidable
    {

        bool IsInteractable { get; set; }
        bool IsSelected     { get; set; }

        bool Visable { get; set; }

        EventArg OnClick { get; set; }

        Color DefaultColor { get; set; }

        void SetOnClick(EventArg @event);

        void Click();

        void Select();
        void Deselect();

        void MouseEnter();
        void MouseExit();

    }

}
