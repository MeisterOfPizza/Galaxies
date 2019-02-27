using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UIPlanetarySystem : UIGroup, IInteractable
    {

        #region IInteractable

        public bool IsInteractable { get; set; } = true;
        public bool IsSelected     { get; set; }

        public EventArg OnClick { get; set; }

        public Color DefaultColor { get; set; }

        #endregion

        public UIPlanetarySystem(Transform transform, Texture2D sprite, EventArg onClick, Screen screen, IVisitable visitable) : base(transform, sprite, screen)
        {
            transform.Size = new Vector2(300, 200);

            AddUIElement(new UIElement(new Transform(new Vector2(-125, -75), new Vector2(50)), visitable.VisitableTypeIcon, screen));
            AddUIElement(new UIText(new Transform(new Vector2(25, -75), new Vector2(250, 50)), SpriteHelper.Arial_Font, visitable.Name, TextAlign.MiddleLeft, 5, screen));
            AddUIElement(new UIText(new Transform(new Vector2(0, 25), new Vector2(300, 150)), SpriteHelper.Arial_Font, visitable.Description, TextAlign.TopLeft, 5, screen));

            this.OnClick      = onClick;
            this.DefaultColor = Color.White;
        }

        #region IInteractable

        public void SetOnClick(EventArg @event)
        {
            this.OnClick = @event;
        }

        public void Click()
        {
            if (OnClick != null)
            {
                OnClick.Invoke();
            }
        }

        public void Select()
        {
            color = Color.Red;

            IsSelected = true;
        }

        public void Deselect()
        {
            color = DefaultColor;

            IsSelected = false;
        }

        public void MouseEnter()
        {
            Select();
        }

        public void MouseExit()
        {
            Deselect();
        }

        #endregion

    }

}
