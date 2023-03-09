using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NaiveAPI
{
    namespace GameTickSystem
    {
        [System.Serializable]
        public class TickUpdateInfo
        {
            public int UpdateID = -1;
            public int UpdateFrequency = 1;
        }
    }
}
