using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    public static class ColorExtension
    {
        public static Color A(this Color color, float a)
        {
            color.a = a;
            return color;
        }
    }
}
