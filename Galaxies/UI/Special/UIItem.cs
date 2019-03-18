using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Items;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UIItem : UIGroup
    {

        public Item Item { get; private set; }

        public UIItem(Transform transform, Texture2D sprite, Screen screen, Item item) : base(transform, sprite, screen)
        {
            this.Item = item;

            Vector2 halfSize = new Vector2(transform.Width / 2f, transform.Height / 2f);

            //Item icon
            AddUIElement(new UIElement(
                new Transform(-halfSize + new Vector2(25), new Vector2(50)),
                SpriteHelper.GetSprite(item.Data.SpriteName),
                screen
                ));

            //Item name
            AddUIElement(new UIText(
                new Transform(new Vector2(25, -halfSize.Y + 25), new Vector2(transform.Width - 50, 50)),
                SpriteHelper.Arial_Font,
                item.Data.Name,
                TextAlign.MiddleLeft,
                5,
                screen
                ));

            //Item description
            AddUIElement(new UIText(
                new Transform(new Vector2(0, transform.Height / 2f - (transform.Height - 50) / 2f), new Vector2(transform.Width, transform.Height - 50)),
                SpriteHelper.Arial_Font,
                item.Data.Description,
                TextAlign.TopLeft,
                5,
                screen
                ));
        }

        public void CreatePurchaseButton(EventArg onPurchase)
        {
            AddUIElement(new UIButton(
                new Transform(new Vector2(transform.Width / 2f - 125, transform.Height / 2f - 25), new Vector2(250, 50)),
                SpriteHelper.Arial_Font,
                "Buy [" + Item.Data.GalacticGoldValue + " GG]",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite(Item.Data.SpriteName),
                onPurchase,
                screen
                ));
        }

        public void CreateSellButton(EventArg onSell)
        {
            AddUIElement(new UIButton(
                new Transform(new Vector2(transform.Width / 2f - 125, transform.Height / 2f - 25), new Vector2(250, 50)),
                SpriteHelper.Arial_Font,
                "Sell [" + Item.Data.GalacticGoldValue + " GG]",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite(Item.Data.SpriteName),
                onSell,
                screen
                ));
        }

        public void CreateUseButton(EventArg onUse)
        {
            AddUIElement(new UIButton(
                new Transform(new Vector2(transform.Width / 2f - 125, transform.Height / 2f - 25), new Vector2(250, 50)),
                SpriteHelper.Arial_Font,
                "Use",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite(Item.Data.SpriteName),
                onUse,
                screen
                ));
        }

    }

}
