using Random = UnityEngine.Random;

namespace Essentials.Extensions
{

    public static class FloatExtensions
    {
        public static float Rand(this float value)
        {
            return Random.Range(0f, value);
        }

        public static bool Dice(this float value)
        {
            return Random.Range(0f, 1f) < value;
        }

        public static bool Dice(this float value, float Min, float Max)
        {
            return Random.Range(Min, Max) < value;
        }

        public static bool Dice(this int value, int Min, int Max)
        {
            return Random.Range(Min, Max) < value;
        }

        public static float CentimeterToInch(this float cm)
        {
            return cm * 0.393700787f;
        }
        
        public static (int feet, float inches) CentimeterToFeet(this float cm)
        {
            float feetRaw = 0.0328f * cm;
            int feet = (int) feetRaw;
            float inches = (feetRaw - feet) * 12;

            return (feet, inches);
        }

        public static string CentimeterToFeetStringified(this float cm)
        {
            float feetRaw = 0.0328f * cm;
            int feet = (int) feetRaw;
            int inches = (int)((feetRaw - feet) * 12);

            return $"{feet}'{inches}";
        }

        public static float KilogramToLb(this float kg)
        {
            return kg * 2.2046f;
        }

        public static string KilogramToLbStringified(this float kg)
        {
            return $"{(int)(kg * 2.2046)}";
        }

        public static float AngleDifference(float angle1, float angle2)
        {
            float diff = (angle2 - angle1 + 180) % 360 - 180;
            return diff < -180 ? diff + 360 : diff;
        }
    }

}