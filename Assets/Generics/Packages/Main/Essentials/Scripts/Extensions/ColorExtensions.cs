using UnityEngine;

namespace Essentials.Extensions
{

    public static class ColorExtensions
    {

        public static Color Alpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

    }

}