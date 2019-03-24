using Galaxies.Extensions;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Galaxies.Controllers
{

    static class AudioController
    {

        public static Song CurrentBackgroundSong { get; private set; }

        public static float MasterVolume { get; private set; } = 1;

        private static float musicVolume = 1;
        public  static float MusicVolume { get { return MediaPlayer.Volume; } }

        private static float effectsVolume = 1;
        public  static float EffectsVolume { get { return SoundEffect.MasterVolume; } }

        public static void Initialize()
        {
            MediaPlayer.Volume       = musicVolume;
            SoundEffect.MasterVolume = effectsVolume;
        }

        public static void SetMasterVolume(float volume)
        {
            MasterVolume = volume;

            MediaPlayer.Volume       = musicVolume * MasterVolume;
            SoundEffect.MasterVolume = effectsVolume * MasterVolume;
        }

        public static void SetMusicVolume(float volume)
        {
            musicVolume = volume;

            MediaPlayer.Volume = musicVolume * MasterVolume;
        }

        public static void SetEffectsVolume(float volume)
        {
            effectsVolume = volume;

            SoundEffect.MasterVolume = effectsVolume * MasterVolume;
        }

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
