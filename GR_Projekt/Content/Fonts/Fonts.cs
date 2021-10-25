using System;

namespace GR_Projekt.Content.Fonts
{
    public static class Fonts
    {
        private static string _fontsPath = "Fonts/";

        //null - regular
        public static string Arial(int fontSize, FontStyleEnumeration? fontStyle = null) => _fontsPath + "Arial" + fontSize + fontStyle;
        
    }
}
