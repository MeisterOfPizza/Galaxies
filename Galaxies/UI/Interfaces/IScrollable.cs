namespace Galaxies.UI.Interfaces
{

    /// <summary>
    /// Marks this UI Element as a scrollable.
    /// </summary>
    interface IScrollable
    {

        bool IsScrollable { get; set; }

        void MouseScroll(int value);

    }

}
