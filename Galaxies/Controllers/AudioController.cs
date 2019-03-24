using Galaxies.Extensions;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Galaxies.Controllers
{

    static class AudioController
    {

        public static Song CurrentBackgroundSong { get; private set; }

        public static void PlayBackgroundSong(string name)
        {
            CurrentBackgroundSong = ContentHelper.GetSong("Audio/Background/" + name);

            MediaPlayer.Play(CurrentBackgroundSong);
            MediaPlayer.IsRepeating = true;
        }

        public static void PlaySoundEffect(string name, float volume = 1, float pitch = 0, float pan = 0)
        {
            SoundEffect soundEffect = ContentHelper.GetSoundEffect("Audio/Sound Effects/" + name);

            soundEffect.Play(volume, pitch, pan);
        }

    }

}
