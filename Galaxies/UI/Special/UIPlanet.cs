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

    class UIPlanet : UIGroup, IInteractable, IMaskable
    {

        #region IInteractable

        public bool IsInteractable { get; set; } = true;
        public bool IsSelected     { get; set; }

        public EventArg OnClick { get; set; }

        public Color DefaultColor { get; set; }

        #endregion

        #region IMaskable

        public bool IsInteractableAfterMask
        {
            get
            {
                return isInteractableAfterMask;
            }
        }

        private bool isInteractableAfterMask;

        #endregion

        public UIPlanet(Transform transform, Texture2D sprite, EventArg onClick, Screen screen, Planet planet) : base(transform, sprite, screen)
        {
            transform.Size = new Vector2(300, 200);

            SetColor(new Color(48, 48, 48));

            this.OnClick      = onClick;
            this.DefaultColor = color;

            //Planet name:
            AddUIElement(new UIText(
                new Transform(new Vector2(0, -75), new Vector2(300, 50)),
                ContentHelper.Arial_Font,
                planet.Data.Name,
                TextAlign.MiddleLeft,
                5,
                screen
                ));

            //Planet description:
            AddUIElement(new UIText(
                new Transform(new Vector2(0, 25), new Vector2(300, 150)),
                ContentHelper.Arial_Font,
                planet.Data.Description,
                TextAlign.TopLeft,
                5,
                screen
                ));
        }

        #region Overriden methods

        public override void SetColor(Color color)
        {
            base.SetColor(color);

            DefaultColor = color;
        }

        #endregion

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
            color = new Color(DefaultColor * 0.9f, 1f);

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

        #region IMaskable

        public void CheckMask(Rectangle mask)
        {
            isInteractableAfterMask = mask.Intersects(transform.Collider);
            IsInteractable = isInteractableAfterMask;
        }

        #endregion

    }

}
