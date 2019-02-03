using Galaxies.UI.Interfaces;
using Microsoft.Xna.Framework;
using System.Text;

namespace Galaxies.UI
{

    enum TextAlign
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    static class TextHelper
    {

        /// <summary>
        /// Returns the text position. Does NOT take local position into account.
        /// </summary>
        public static Vector2 Align<T>(T textElement) where T : UIElement, ITextElement<T>
        {
            Vector2 size = textElement.SpriteFont.MeasureString(textElement.Text);

            switch (textElement.TextAlign)
            {
                case TextAlign.TopLeft:
                    return new Vector2(textElement.TextPadding, textElement.TextPadding);
                case TextAlign.TopCenter:
                    return new Vector2(textElement.Width / 2f - size.X / 2f, textElement.TextPadding);
                case TextAlign.TopRight:
                    return new Vector2(textElement.Width - size.X - textElement.TextPadding, textElement.TextPadding);
                case TextAlign.MiddleLeft:
                    return new Vector2(textElement.TextPadding, textElement.Height / 2f - size.Y / 2f);
                case TextAlign.MiddleCenter:
                    return new Vector2(textElement.Width / 2f - size.X / 2f, textElement.Height / 2f - size.Y / 2f);
                case TextAlign.MiddleRight:
                    return new Vector2(textElement.Width - size.X - textElement.TextPadding, textElement.Height / 2f - size.Y / 2f);
                case TextAlign.BottomLeft:
                    return new Vector2(textElement.TextPadding, textElement.Height - size.Y / 2f);
                case TextAlign.BottomCenter:
                    return new Vector2(textElement.Width / 2f - size.X / 2f, textElement.Height - size.Y / 2f);
                case TextAlign.BottomRight:
                    return new Vector2(textElement.Width - size.X - textElement.TextPadding, textElement.Height - size.Y / 2f);
            }

            return Vector2.Zero;
        }

        /// <summary>
        /// Wraps the text.
        /// </summary>
        public static string WrapText<T>(T textElement, float maxLineWidth) where T : UIElement, ITextElement<T>
        {
            //If the text should align in the middle, then we want to decrease the size of each text line by TextPadding * 2.
            //This is because we cannot push the text in any direction (because it's in the middle, hence Center).
            switch (textElement.TextAlign)
            {
                case TextAlign.TopCenter:
                case TextAlign.MiddleCenter:
                case TextAlign.BottomCenter:
                    maxLineWidth -= textElement.TextPadding * 2;
                    break;
                default:
                    maxLineWidth -= textElement.TextPadding;
                    break;
            }

            string[] words = textElement.Text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = textElement.SpriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = textElement.SpriteFont.MeasureString(word); //Size of word

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else //Line break
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }

    }

}
