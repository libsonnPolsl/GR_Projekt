using System;
namespace GR_Projekt.States.Settings.Models
{
    public class DefaultGameSetting
    {
        public static SettingsModel getDefaultSettings => new SettingsModel
        {
            musicVolume = 0.5f,
            soundsVolume = 0.5f,
            fullscreen = false,
            width = 800,
            height = 600,
        };
    }
}
