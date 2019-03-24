using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Screens
{

    class SettingsScreen : Screen
    {

        UISlider masterVolumeSlider;
        UISlider musicVolumeSlider;
        UISlider effectsVolumeSlider;

        public override void CreateUI()
        {
            //Background:
            AddUIElement(new UIBackgroundAnimation(
                new Transform(Alignment.MiddleCenter, GameUIController.WindowSize),
                ContentHelper.Space_Background_Animation_1,
                this
                ));

            //Fullscreen title:
            AddUIElement(new UIText(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(-250, -55), new Vector2(200, 50)), new Vector2(200, 50)),
                ContentHelper.Arial_Font,
                "Fullscreen",
                TextAlign.MiddleLeft,
                0,
                this
                ));

            //Fullscreen checkbox:
            AddUIElement(new UICheckbox(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(350, -55), new Vector2(50, 50)), new Vector2(50, 50)),
                ContentHelper.Box4x4_Sprite,
                ContentHelper.Bullet_Sprite,
                new EventArg0(MainGame.Singleton.Graphics.ToggleFullScreen),
                true,
                this
                ));

            //Master volume title:
            AddUIElement(new UIText(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(-250, 0), new Vector2(200, 50)), new Vector2(200, 50)),
                ContentHelper.Arial_Font,
                "Master volume",
                TextAlign.MiddleLeft,
                0,
                this
                ));

            //Master volume slider:
            masterVolumeSlider = AddUIElement(new UISlider(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(250, 0), new Vector2(250, 10)), new Vector2(250, 10)),
                ContentHelper.Box4x4_Sprite,
                ContentHelper.Bullet_Sprite,
                null,
                AudioController.MasterVolume,
                this
                ));

            masterVolumeSlider.SetOnClick(new EventArg2<_EventArg1<float>, UISlider>(SetVolume, AudioController.SetMasterVolume, masterVolumeSlider));

            //Music volume title:
            AddUIElement(new UIText(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(-250, 55), new Vector2(200, 50)), new Vector2(200, 50)),
                ContentHelper.Arial_Font,
                "Music volume",
                TextAlign.MiddleLeft,
                0,
                this
                ));

            //Music volume slider:
            musicVolumeSlider = AddUIElement(new UISlider(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(250, 55), new Vector2(250, 10)), new Vector2(250, 10)),
                ContentHelper.Box4x4_Sprite,
                ContentHelper.Bullet_Sprite,
                null,
                AudioController.MusicVolume,
                this
                ));

            musicVolumeSlider.SetOnClick(new EventArg2<_EventArg1<float>, UISlider>(SetVolume, AudioController.SetMusicVolume, musicVolumeSlider));

            //Effects volume title:
            AddUIElement(new UIText(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(-250, 110), new Vector2(200, 50)), new Vector2(200, 50)),
                ContentHelper.Arial_Font,
                "Effects volume",
                TextAlign.MiddleLeft,
                0,
                this
                ));

            //Effects volume slider:
            effectsVolumeSlider = AddUIElement(new UISlider(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(250, 110), new Vector2(250, 10)), new Vector2(250, 10)),
                ContentHelper.Box4x4_Sprite,
                ContentHelper.Bullet_Sprite,
                null,
                AudioController.EffectsVolume,
                this
                ));

            effectsVolumeSlider.SetOnClick(new EventArg2<_EventArg1<float>, UISlider>(SetVolume, AudioController.SetEffectsVolume, effectsVolumeSlider));

            /* The resolution elements are placed near the bottom to allow them to overlap all other elements. */

            //Resolution title:
            AddUIElement(new UIText(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(-250, -110), new Vector2(200, 50)), new Vector2(200, 50)),
                ContentHelper.Arial_Font,
                "Resolution",
                TextAlign.MiddleLeft,
                0,
                this
                ));

            //Resolution dropdown:
            SetupResolutionDropdownOption(AddUIElement(new UIDropdown(
                new Transform(Transform.CreatePosition(Alignment.MiddleCenter, new Vector2(250, -110), new Vector2(250, 50)), new Vector2(250, 50)),
                this,
                0,
                new DropdownOption[]
                {
                    new DropdownOption("1920x1080", new EventArgList(new EventArg2<int, int>(GameUIController.ChangeResolution, 1920, 1080), new EventArg1<EventArg>(GameUIController.CreateSettingsScreen, null))),
                    new DropdownOption("1680x1050", new EventArgList(new EventArg2<int, int>(GameUIController.ChangeResolution, 1680, 1050), new EventArg1<EventArg>(GameUIController.CreateSettingsScreen, null)))
                }
                )));

            //Back to menu button:
            AddUIElement(new UIButton(
                new Transform(Alignment.BottomCenter, new Vector2(250, 50)),
                ContentHelper.Arial_Font,
                "Menu",
                TextAlign.MiddleCenter,
                0,
                ContentHelper.GetSprite("Sprites/UI/column"),
                new EventArg1<EventArg>(GameUIController.CreateMenuScreen, null),
                this
                ));
        }

        private void SetVolume(_EventArg1<float> method, UISlider volumeSlider)
        {
            method.Invoke(volumeSlider.Value);
        }

        private void SetupResolutionDropdownOption(UIDropdown dropdown)
        {
            //What is the current resolution?
            string currentRes = GameUIController.WindowWidth + "x" + GameUIController.WindowHeight;

            foreach (var option in dropdown.Options)
            {
                if (option.Title.Equals(currentRes))
                {
                    //We found the preselected option, appply it:
                    dropdown.SelectOption(option);
                }
            }
        }

    }

}
