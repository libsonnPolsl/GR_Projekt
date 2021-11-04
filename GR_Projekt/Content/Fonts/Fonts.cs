using System;

namespace GR_Projekt.Content.Fonts
{
    public static class Fonts
    {
        private static string _fontsPath = "Fonts/";

        //null - regular
        public static string Copperplate(int fontSize, FontStyleEnumeration? fontStyle = null) => _fontsPath + "Copperplate" + fontSize + fontStyle;

        internal static string Copperplate(int fontSize, object fontStyle)
        {
            throw new NotImplementedException();
        }
    }
}
