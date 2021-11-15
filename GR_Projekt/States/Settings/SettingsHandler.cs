using System;
using System.Diagnostics;
using GR_Projekt.States.Settings.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GR_Projekt.States.Settings
{
    public static class SettingsHandler
    {
        public static bool setSettings(SettingsModel settingsModel, GraphicsDeviceManager graphicsDeviceManager)
        {
            try
            {

                graphicsDeviceManager.PreferredBackBufferHeight = settingsModel.height;
                graphicsDeviceManager.ApplyChanges();
                graphicsDeviceManager.PreferredBackBufferWidth = settingsModel.width;
                graphicsDeviceManager.ApplyChanges();
                graphicsDeviceManager.IsFullScreen = settingsModel.fullscreen;
                graphicsDeviceManager.ApplyChanges();

                MediaPlayer.Volume = (float)settingsModel.musicVolume;
                SoundEffect.MasterVolume = (float)settingsModel.soundsVolume;

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
