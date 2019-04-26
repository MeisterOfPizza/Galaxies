using Microsoft.Xna.Framework;

namespace Galaxies.UI.Interfaces
{

    interface IMaskable
    {

        bool IsInteractableAfterMask { get; }

        void CheckMask(Rectangle mask);

    }

}
