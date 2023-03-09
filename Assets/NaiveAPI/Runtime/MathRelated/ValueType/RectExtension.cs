using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    public static class RectExtension
    {
        public static Rect NextX(this ref Rect rect)
        {
            rect.x = rect.xMax;
            return rect;
        }
        public static Rect NextY(this ref Rect rect)
        {
            rect.y = rect.yMax;
            return rect;
        }

        public static Rect OffsetX(this ref Rect rect, float offset)
        {
            rect.x += offset;
            rect.width -= offset; 
            return rect;
        }
        public static Rect OffsetY(this ref Rect rect, float offset)
        {
            rect.y += offset;
            rect.width -= offset;
            return rect;
        }
    }
}

