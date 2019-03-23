using Microsoft.Xna.Framework.Audio;
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

        public static void PlaySoundEffect(string name, float volume = 1, float pitch = 0, float pan = 0)
        {
            ///TODO: Use <see cref="Extensions.SpriteHelper"/> instead.
            SoundEffect soundEffect = MainGame.Singleton.Content.Load<SoundEffect>("Audio/Sound Effects/" + name);

            soundEffect.Play(volume, pitch, pan);
        }

    }

}
