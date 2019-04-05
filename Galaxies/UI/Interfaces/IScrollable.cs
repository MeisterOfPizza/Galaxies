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

        /// <summary>
        /// The percentage of the view (all items in total) that can be viewed at once.
        /// </summary>
        float DeltaViewSize();

        /// <summary>
        /// Set where the view of the scrollable should be.
        /// Because this is a the middle value, the <paramref name="scrollValue"/> will only be the middle of the view,
        /// therefore you need to determine the same index and then substract and add the index amount to get the full view.
        /// </summary>
        void SetViewMiddleIndex(float scrollValue);

    }

}
