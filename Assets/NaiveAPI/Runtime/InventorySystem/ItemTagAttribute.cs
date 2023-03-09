using System;
using UnityEngine;

namespace NaiveAPI
{
    namespace ItemSystem
    {
        [AttributeUsage(AttributeTargets.All)]
        public class ItemTagAttribute : PropertyAttribute
        {
            public int SelectIndex = 0;
        }
    }
}
