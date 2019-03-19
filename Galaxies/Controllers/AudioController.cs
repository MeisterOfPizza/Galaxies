using Microsoft.Xna.Framework.Media;

namespace Galaxies.Controllers
{

    static class AudioController
    {

        public static Song CurrentBackgroundSong { get; private set; }

        public static void PlayBackgroundSong(string name)
        {
            ///TODO: Use <see cref="Extensions.SpriteHelper"/> instead.
            CurrentBackgroundSong = MainGame.Singleton.Content.Load<Song>("Audio/Background/" + name);

            MediaPlayer.Play(CurrentBackgroundSong);
            MediaPlayer.IsRepeating = true;
        }

    }

}
