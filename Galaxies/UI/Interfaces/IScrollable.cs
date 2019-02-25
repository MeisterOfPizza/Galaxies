namespace Galaxies.UI.Interfaces
{

    interface IScrollable
    {

        bool IsScrollable { get; set; }

        void MouseScroll(int value);

    }

}
