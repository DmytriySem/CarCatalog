using System;
using System.Collections.Generic;
using System.Text;

namespace AuxiliaryClass
{
    public static class Auxiliary
    {
        public enum COLOR { RED, GREEN, BLUE, ORANGE, PURPLE };
        public static float[] VolumeEngine = new float[] { 1.0F, 1.2F, 1.5F, 1.6F, 2.0F, 2.2F, 2.4F, 2.5F, 3.0F, 4.0F };

        public static string[] GetStringMassOfColorEnums()
        {
            string[] colors = new string[Enum.GetNames(typeof(COLOR)).Length];
            int i = 0;
            foreach (var item in Enum.GetValues(typeof(COLOR)))
            {
                colors[i++] = Enum.GetName(typeof(COLOR), item);
            }

            return colors;
        }

        public static string[] GetStringMassOfVolumeEngine()
        {
            string[] volEngines = new string[VolumeEngine.Length];

            int i = 0;
            foreach (var item in VolumeEngine)
            {
                volEngines[i++] = item.ToString();
            }

            return volEngines;
        }


    }
}
