using Galaxies.Core;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UICheckbox : UIElement, IInteractable
    {
        
        Texture2D checkSprite;

        public bool IsChecked { get; private set; }

        #region IInteractable

        public bool     IsInteractable { get; set; } = true;
        public bool     IsSelected     { get; set; }
        public EventArg OnClick        { get; set; }
        public Color    DefaultColor   { get; set; }

        #endregion

        public UICheckbox(Transform transform, Texture2D boxSprite, Texture2D checkSprite, EventArg onCheck, bool isChecked, Screen screen) : base(transform, boxSprite, screen)
        {
            this.checkSprite  = checkSprite;
            this.OnClick      = onCheck;
            this.DefaultColor = this.color;
            this.IsChecked    = isChecked;
        }

        private void ToggleCheck()
        {
            IsChecked = !IsChecked;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visable && checkSprite != null && IsChecked)
            {
                spriteBatch.Draw(checkSprite, new Rectangle(transform.RawX, transform.RawY, transform.RawWidth, transform.RawHeight), null, color, transform.Rotation, Origin, SpriteEffects.None, 0f);
            }
        }

        #region IInteractable

        public void Click()
        {
            ToggleCheck();

            if (OnClick != null)
            {
                OnClick.Invoke();
            }
        }

        public void SetOnClick(EventArg @event)
        {
            OnClick = @event;
        }

        public void Select()
        {
            SetColor(new Color(DefaultColor * 0.9f, 1f));

            IsSelected = true;
        }

        public void Deselect()
        {
            SetColor(DefaultColor);

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
