using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Space
{

    interface IVisitable
    {

        string    Name              { get; }
        string    Description       { get; }
        Texture2D VisitableTypeIcon { get; }

        void Visit();

    }

}
