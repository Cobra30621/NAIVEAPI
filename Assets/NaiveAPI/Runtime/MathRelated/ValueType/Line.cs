using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    namespace MathRelated
    {
        public struct Line
        {
            public Vector2 Begin;
            public Vector2 End;

            public float Length { get { return Vector2.Distance(Begin, End); } }
            public Vector2 Vector { get { return End - Begin; } }
        }
    }
}
