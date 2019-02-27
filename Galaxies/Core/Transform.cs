using Galaxies.UIControllers;
using Microsoft.Xna.Framework;

namespace Galaxies.Core
{

    /// <summary>
    /// Alignment of a GameObject in screen space.
    /// Used by <see cref="Transform"/>.
    /// </summary>
    public enum Alignment
    {
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottomCenter,
        BottomRight,

        Custom
    }

    /// <summary>
    /// Contains the position (and alignment), size and rotation of an object.
    /// </summary>
    public class Transform
    {

        public GameObject GameObject { get; private set; }
        public Rectangle  Collider   { get; private set; }

        /* Fields and properties */

        #region Position
        
        /// <summary>
        /// Position of the <see cref="GameObject"/>.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                SetPosition(value);

                Alignment = Alignment.Custom;
            }
        }

        private Vector2 position;

        /// <summary>
        /// Shorthand for <see cref="Position"/>.X.
        /// </summary>
        public float X { get { return Position.X; } }

        /// <summary>
        /// Shorthand for <see cref="Position"/>.Y.
        /// </summary>
        public float Y { get { return Position.Y; } }

        /// <summary>
        /// <see cref="Position"/>.X in pixels.
        /// </summary>
        public int RawX { get; private set; }

        /// <summary>
        /// <see cref="Position"/>.Y in pixels.
        /// </summary>
        public int RawY { get; private set; }

        /// <summary>
        /// Used to "reset" the position after a window resizing event has occurred.
        /// </summary>
        private Vector2 percentage;

        #endregion

        #region Size

        /// <summary>
        /// Size of the <see cref="GameObject"/>.
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return size;
            }

            set
            {
                SetSize(value);
            }
        }

        private Vector2 size;

        /// <summary>
        /// X value of <see cref="Size"/>.
        /// </summary>
        public float Width { get; private set; }

        /// <summary>
        /// Y value of <see cref="Size"/>.
        /// </summary>
        public float Height { get; private set; }

        /// <summary>
        /// <see cref="Size"/>.X in pixels.
        /// </summary>
        public int RawWidth { get; private set; }

        /// <summary>
        /// <see cref="Size"/>.Y in pixels.
        /// </summary>
        public int RawHeight { get; private set; }

        #endregion

        #region Rotation

        /// <summary>
        /// Rotation of the <see cref="GameObject"/>.
        /// </summary>
        public float Rotation { get; set; }

        #endregion

        #region Alignment

        /// <summary>
        /// Alignment of the <see cref="GameObject"/> in screen space.
        /// </summary>
        public Alignment Alignment
        {
            get
            {
                return alignment;
            }

            set
            {
                SetAlignment(value);
            }
        }

        private Alignment alignment = Alignment.Custom;

        #endregion

        #region Helpers

        /// <summary>
        /// Half of <see cref="Size"/>.
        /// </summary>
        private Vector2 halfSize;

        #endregion

        /* Methods */

        #region Constructors

        public Transform(Vector2 size)
        {
            SetSize(size);
        }

        public Transform(Vector2 position, Vector2 size)
        {
            SetPosition(position);
            SetSize(size);
        }

        public Transform(Vector2 position, Vector2 size, float rotation)
        {
            SetPosition(position);
            SetSize(size);

            this.Rotation = rotation;
        }

        public Transform(Alignment alignment, Vector2 size)
        {
            this.alignment = alignment;
            SetSize(size);
        }

        public Transform(Alignment alignment, Vector2 size, float rotation)
        {
            this.alignment = alignment;
            SetSize(size);

            this.Rotation = rotation;
        }

        #endregion

        #region Position

        /// <summary>
        /// Sets the position, raw x and y and percentage.
        /// Also calls the <see cref="GameObject.PositionChanged"/> method.
        /// </summary>
        private void SetPosition(Vector2 position)
        {
            this.position = position;

            RawX = (int)position.X;
            RawY = (int)position.Y;

            percentage = new Vector2(RawX / (float)GameUIController.WindowWidth, RawY / (float)GameUIController.WindowHeight);

            CreateCollider();

            if (GameObject != null)
            {
                GameObject.PositionChanged();
            }
        }

        #endregion

        #region Size

        /// <summary>
        /// Sets the size and updates the alignment.
        /// Also calls the <see cref="GameObject.SizeChanged"/> method.
        /// </summary>
        private void SetSize(Vector2 size)
        {
            this.size = size;
            halfSize  = size / 2f;

            this.Width  = size.X;
            this.Height = size.Y;

            this.RawWidth  = (int)size.X;
            this.RawHeight = (int)size.Y;

            SetAlignment(alignment);

            if (GameObject != null)
            {
                GameObject.SizeChanged();
            }
        }

        /// <summary>
        /// Sets the x value (width) of <see cref="Size"/>.
        /// </summary>
        /// <param name="x">New width</param>
        public void SetSizeX(float x)
        {
            SetSize(new Vector2(x, size.Y));
        }

        /// <summary>
        /// Sets the y value (height) of <see cref="Size"/>.
        /// </summary>
        /// <param name="y">New height</param>
        public void SetSizeY(float y)
        {
            SetSize(new Vector2(size.X, y));
        }

        #endregion

        #region Alignment

        /// <summary>
        /// Aligns the position based on <see cref="Alignment"/>.
        /// If <see cref="Alignment"/> == <see cref="Alignment.Custom"/> then the position is not changed.
        /// </summary>
        /// <param name="alignment"></param>
        private void SetAlignment(Alignment alignment)
        {
            this.alignment = alignment;

            switch (this.alignment)
            {
                case Alignment.TopLeft:
                    SetPosition(halfSize);
                    break;
                case Alignment.TopCenter:
                    SetPosition(new Vector2(GameUIController.WindowWidth / 2f, halfSize.Y));
                    break;
                case Alignment.TopRight:
                    SetPosition(new Vector2(GameUIController.WindowWidth - halfSize.X, halfSize.Y));
                    break;
                case Alignment.MiddleLeft:
                    SetPosition(new Vector2(halfSize.X, GameUIController.WindowHeight / 2f));
                    break;
                case Alignment.MiddleCenter:
                    SetPosition(new Vector2(GameUIController.WindowWidth / 2f, GameUIController.WindowHeight / 2f));
                    break;
                case Alignment.MiddleRight:
                    SetPosition(new Vector2(GameUIController.WindowWidth - halfSize.X, GameUIController.WindowHeight / 2f));
                    break;
                case Alignment.BottomLeft:
                    SetPosition(new Vector2(halfSize.X, GameUIController.WindowHeight - halfSize.Y));
                    break;
                case Alignment.BottomCenter:
                    SetPosition(new Vector2(GameUIController.WindowWidth / 2f, GameUIController.WindowHeight - halfSize.Y));
                    break;
                case Alignment.BottomRight:
                    SetPosition(new Vector2(GameUIController.WindowWidth - halfSize.X, GameUIController.WindowHeight - halfSize.Y));
                    break;
                default:
                case Alignment.Custom:
                    CreateCollider(); //Create a new collider, just in case a change in size has occurred.
                    //Do nothing, the GameObject stays in the same position.
                    break;
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Sets the <see cref="GameObject"/>. This should only be called internally be the newly created GameObject itself.
        /// </summary>
        public void SetGameObject(GameObject gameObject)
        {
            this.GameObject = gameObject;

            CreateCollider();
        }

        /// <summary>
        /// Creates the collider.
        /// </summary>
        private void CreateCollider()
        {
            Collider = new Rectangle((Position - halfSize).ToPoint(), Size.ToPoint());
        }

        #endregion

    }

}
