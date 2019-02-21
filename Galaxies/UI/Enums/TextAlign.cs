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
            Vector2 size = textElement.SpriteFont.MeasureString(textElement.FormattedText);

            textElement.FormattedTextOrigin = size / 2f;

            float xBorder = -textElement.Width / 2f + size.X / 2f + textElement.TextPadding; //Default = left //This is the x coordinate for the left align
            float yBorder = -textElement.Height / 2f + textElement.TextPadding + size.Y / 2f; //Default = top //This is the y coordinate for the top align

            //Let's use these x/y Borders and just invert them (* -1) when we need to draw on the opposite side of the align.
            //They're called border because it's the closest that we can get to the text elements border.

            switch (textElement.TextAlign)
            {
                case TextAlign.TopLeft:
                    return new Vector2(xBorder, yBorder);
                case TextAlign.TopCenter:
                    return new Vector2(0, yBorder);
                case TextAlign.TopRight:
                    return new Vector2(-xBorder, yBorder);
                case TextAlign.MiddleLeft:
                    return new Vector2(xBorder, 0);
                case TextAlign.MiddleCenter:
                    return Vector2.Zero;
                case TextAlign.MiddleRight:
                    return new Vector2(-xBorder, 0);
                case TextAlign.BottomLeft:
                    return new Vector2(xBorder, -yBorder);
                case TextAlign.BottomCenter:
                    return new Vector2(0, -yBorder);
                case TextAlign.BottomRight:
                    return new Vector2(-xBorder, -yBorder);
            }

            return Vector2.Zero; //Should not be able to happen...
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
