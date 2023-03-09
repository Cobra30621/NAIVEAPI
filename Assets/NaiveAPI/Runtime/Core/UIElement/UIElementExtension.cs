using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class UIElementExtension
{
    public static void SetMarginAnchor(this IStyle element, TextAnchor anchor, float marginPixel = 5)
    {
        StyleLength auto = new StyleLength(StyleKeyword.Auto);
        StyleLength pixel = new StyleLength(new Length(marginPixel, LengthUnit.Pixel));
        element.marginTop = auto;
        element.marginBottom = auto;
        element.marginLeft = auto;
        element.marginRight = auto;
        switch (anchor)
        {
            case TextAnchor.UpperLeft:
                element.marginTop = pixel;
                element.marginLeft = pixel;
                break;
            case TextAnchor.UpperCenter:
                element.marginTop = pixel;
                break;
            case TextAnchor.UpperRight:
                element.marginTop = pixel;
                element.marginRight = pixel;
                break;
            case TextAnchor.MiddleLeft:
                element.marginLeft = pixel;
                break;
            case TextAnchor.MiddleCenter:
                break;
            case TextAnchor.MiddleRight:
                element.marginRight = pixel;
                break;
            case TextAnchor.LowerLeft:
                element.marginBottom = pixel;
                element.marginLeft = pixel;
                break;
            case TextAnchor.LowerCenter:
                element.marginBottom = pixel;
                break;
            case TextAnchor.LowerRight:
                element.marginBottom = pixel;
                element.marginRight = pixel;
                break;
        }
    }

}
