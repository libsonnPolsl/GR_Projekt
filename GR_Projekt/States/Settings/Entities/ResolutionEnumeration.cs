using System;
using System.Collections.Generic;

namespace GR_Projekt.States.Settings.Entities
{
    public enum ResolutionEnumeration
    {
        Res800x600, Res1280x720, Res1920x1080
    }


    public static class ResolutionEnumerationParser
    {
        public static Dictionary<string, int> toMap(ResolutionEnumeration resolutionEnumeration)
        {
            Dictionary<string, int> _map = new Dictionary<string, int>();
            string _resolutionValues = resolutionEnumeration.ToString().Replace("Res", "");
            string[] _values = _resolutionValues.Split("x");

            _map.Add("width", int.Parse(_values[0]));
            _map.Add("height", int.Parse(_values[1]));
            return _map;
        }

        public static string toString(ResolutionEnumeration resolutionEnumeration)
        {
            return resolutionEnumeration.ToString().Replace("Res", "");

        }

        public static ResolutionEnumeration fromValues(int width, int height)
        {
            string _mapToString = "Res" + width.ToString() + "x" + height.ToString();

            return (ResolutionEnumeration)Enum.Parse(typeof(ResolutionEnumeration), _mapToString);

        }

        public static int indexOfValue(ResolutionEnumeration resolutionEnumeration)
        {
            return (int)resolutionEnumeration;
        }

    }
}
