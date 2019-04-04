using Galaxies.Core.Interfaces;

namespace Galaxies.UI.Interfaces
{

    /// <summary>
    /// Marks this UI Element as a scrollable.
    /// </summary>
    interface IScrollable : ICollidable
    {

        bool IsScrollable { get; set; }

        bool Visable { get; set; }

        void MouseScroll(int value);

    }

}
