using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using GR_Projekt.Core;

namespace GR_Projekt.States.Settings.Models
{
    public class SettingsModel
    {
        public float musicVolume { get; set; }
        public float soundsVolume { get; set; }
        public bool fullscreen { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public SettingsModel() { }

        public static SettingsModel fromMemory()
        {
            try
            {
                string fileName = FilesPaths.getSettingsFilePath;
                string jsonString = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<SettingsModel>(jsonString);
            }
            catch (Exception)
            {
                return DefaultGameSetting.getDefaultSettings;
            }
        }

        public bool toMemory()
        {
            try
            {
                string fileName = FilesPaths.getSettingsFilePath;
                string jsonString = JsonSerializer.Serialize(this);
                File.WriteAllText(fileName, jsonString);

                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
