using System.Collections.Generic;

namespace Galaxies.UI.Interfaces
{

    /// <summary>
    /// Marks this UI Element as a container.
    /// </summary>
    interface IContainer
    {

        IList<UIElement> Children { get; }

    }

}
