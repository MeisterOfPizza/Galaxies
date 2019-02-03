using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Interfaces
{

    interface ITextElement<T> where T : UIElement
    {

        SpriteFont SpriteFont    { get; }
        TextAlign  TextAlign     { get; }
        string     Text          { get; set; }
        string     FormattedText { get; set; }
        int        TextPadding   { get; set; }

    }

}
