using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Items;
using Galaxies.UI.Elements;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UIItem : UIGroup, IMaskable
    {

        public Item Item { get; private set; }

        private UIButton eventButton;

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

        public UIItem(Transform transform, Texture2D sprite, Screen screen, Item item) : base(transform, sprite, screen)
        {
            this.Item = item;

            SetColor(new Color(48, 48, 48));

            Vector2 halfSize = new Vector2(transform.Width / 2f, transform.Height / 2f);

            //Item icon
            AddUIElement(new UIElement(
                new Transform(-halfSize + new Vector2(30), new Vector2(50)),
                Item.ItemIcon,
                screen
                ));

            //Item name
            AddUIElement(new UIText(
                new Transform(new Vector2(30, -halfSize.Y + 30), new Vector2(transform.Width - 60, 50)),
                ContentHelper.Arial_Font,
                item.Data.Name,
                TextAlign.MiddleLeft,
                5,
                screen
                ));

            //Item description
            AddUIElement(new UIText(
                new Transform(new Vector2(0, transform.Height / 2f - (transform.Height - 55) / 2f), new Vector2(transform.Width, transform.Height - 55)),
                ContentHelper.Arial_Font,
                item.Data.Description,
                TextAlign.TopLeft,
                5,
                screen
                ));
        }

        public void CreatePurchaseButton(EventArg onPurchase)
        {
            eventButton = AddUIElement(new UIButton(
                new Transform(new Vector2(transform.Width / 2f - 130, transform.Height / 2f - 30), new Vector2(250, 50)),
                ContentHelper.Arial_Font,
                "Buy [" + Item.Data.GalacticGoldValue + " GG]",
                TextAlign.MiddleCenter,
                5,
                ContentHelper.Box4x4_Sprite,
                onPurchase,
                screen
                ));

            eventButton.SetColor(new Color(28, 28, 28));
        }

        public void CreateSellButton(EventArg onSell)
        {
            eventButton = AddUIElement(new UIButton(
                new Transform(new Vector2(transform.Width / 2f - 130, transform.Height / 2f - 30), new Vector2(250, 50)),
                ContentHelper.Arial_Font,
                "Sell [" + Item.Data.GalacticGoldValue + " GG]",
                TextAlign.MiddleCenter,
                5,
                ContentHelper.Box4x4_Sprite,
                onSell,
                screen
                ));

            eventButton.SetColor(new Color(28, 28, 28));
        }

        public void CreateUseButton(EventArg onUse)
        {
            eventButton = AddUIElement(new UIButton(
                new Transform(new Vector2(transform.Width / 2f - 130, transform.Height / 2f - 30), new Vector2(250, 50)),
                ContentHelper.Arial_Font,
                "Use",
                TextAlign.MiddleCenter,
                5,
                ContentHelper.Box4x4_Sprite,
                onUse,
                screen
                ));

            eventButton.SetColor(new Color(28, 28, 28));
        }

        #region IMaskable

        public void CheckMask(Rectangle mask)
        {
            isInteractableAfterMask = mask.Intersects(eventButton.Transform.Collider);
            eventButton.IsInteractable = isInteractableAfterMask;
        }

        #endregion

    }

}
