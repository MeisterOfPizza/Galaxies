using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Galaxies.UI.Elements
{

    class UIVideo : UIElement
    {

        private Video       video;
        private VideoPlayer videoPlayer;

        public UIVideo(Transform transform, Video video, Screen screen) : base(transform, null, screen)
        {
            this.video = video;

            videoPlayer = new VideoPlayer();
            videoPlayer.Play(video);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite = videoPlayer.GetTexture();

            base.Draw(spriteBatch);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            videoPlayer.Stop();
            videoPlayer.Dispose();
        }

    }

}
