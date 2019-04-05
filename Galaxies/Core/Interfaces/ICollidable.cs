using Microsoft.Xna.Framework;

namespace Galaxies.Core.Interfaces
{

    /// <summary>
    /// Forces the object to use the <see cref="Rectangle.Contains(Vector2)"/> and <see cref="Rectangle.Intersects(Rectangle)"/> methods.
    /// </summary>
    interface ICollidable
    {

        Transform Transform { get; }

        bool Contains(Vector2 position);
        bool Intersects(GameObject other);

    }

}
