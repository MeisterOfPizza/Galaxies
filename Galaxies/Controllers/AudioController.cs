using Galaxies.Extensions;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Galaxies.Controllers
{

    static class AudioController
    {

        public static Song CurrentBackgroundSong { get; private set; }

        public static float MasterVolume { get; private set; } = 1;

        private static float musicVolume         = 1;
        private static float modifiedMusicVolume = musicVolume * MasterVolume;
        public  static float MusicVolume         { get { return musicVolume; } }

        private static float effectsVolume         = 1;
        private static float modifiedEffectsVolume = effectsVolume * MasterVolume;
        public  static float EffectsVolume         { get { return effectsVolume; } }

        public static void Initialize()
        {
            MediaPlayer.Volume       = modifiedMusicVolume;
            SoundEffect.MasterVolume = modifiedEffectsVolume;
        }

        public static void SetMasterVolume(float volume)
        {
            MasterVolume = volume;

            modifiedMusicVolume   = musicVolume * MasterVolume;
            modifiedEffectsVolume = effectsVolume * MasterVolume;

            MediaPlayer.Volume       = modifiedMusicVolume;
            SoundEffect.MasterVolume = modifiedEffectsVolume;
        }

        public static void SetMusicVolume(float volume)
        {
            musicVolume = volume;
            modifiedMusicVolume = musicVolume * MasterVolume;

            MediaPlayer.Volume = modifiedMusicVolume;
        }

        public static void SetEffectsVolume(float volume)
        {
            effectsVolume = volume;
            modifiedEffectsVolume = effectsVolume * MasterVolume;

            SoundEffect.MasterVolume = modifiedEffectsVolume;
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
