﻿using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Items;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Galaxies.UI.Special
{

    class UIInventory : UIGroup
    {

        public UIScrollableGrid ItemGrid { get; private set; }

        public UIItem[] Items { get; private set; }

        public UIInventory(Transform transform, Screen screen, IList<Item> items, string title, bool closeButton, EventArg onClose) : base(transform, null, screen)
        {
            SetColor(new Color(56, 56, 56));

            AddUIElement(new UIText(
                new Transform(new Vector2(0, -transform.Height / 2f - 50), new Vector2(transform.Width, 50)),
                ContentHelper.Arial_Font,
                title,
                TextAlign.MiddleLeft,
                0,
                screen
                ));

            ItemGrid = AddUIElement(new UIScrollableGrid(
                new Transform(transform.Size),
                ContentHelper.Box4x4_Sprite,
                screen,
                new Vector4(10),
                new Vector2(5)
                ));

            ItemGrid.SetColor(new Color(56, 56, 56));

            Items = new UIItem[items.Count];

            for (int i = 0; i < items.Count; i++)
            {
                var itemPointer = items[i]; //Create a new pointer (reference).

                Items[i] = ItemGrid.AddUIElement(new UIItem(
                    new Transform(new Vector2(400, 300)),
                    ContentHelper.Box4x4_Sprite,
                    screen,
                    itemPointer
                    ));
            }

            if (closeButton)
            {
                AddUIElement(new UIButton(
                    new Transform(new Vector2(0, transform.Height / 2f + 50), new Vector2(100, 50)),
                    ContentHelper.Arial_Font,
                    "Close",
                    TextAlign.MiddleCenter,
                    0,
                    ContentHelper.Box4x4_Sprite,
                    onClose,
                    screen
                )).SetColor(new Color(48, 48, 48));
            }
        }

    }

}
