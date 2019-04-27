using Galaxies.Core.Interfaces;

namespace Galaxies.UI.Interfaces
{

    /// <summary>
    /// Marks this UI Element as a scrollable.
    /// </summary>
    interface IScrollable : ICollidable
    {

        bool  IsScrollable { get; set; }
        bool  Visable      { get; set; }
        float ScrollValue  { get; set; }

        void MouseScroll(int value);

        /// <summary>
        /// The percentage of the view (all items in total) that can be viewed at once.
        /// </summary>
        float DeltaViewSize();

    }

}
