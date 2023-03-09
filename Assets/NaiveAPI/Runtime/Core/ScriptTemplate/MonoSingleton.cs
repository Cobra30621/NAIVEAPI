using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    public class MonoSingleton<T> : MonoBehaviour
    {
        internal static T instance;
        public static T Instance
        {
            get
            {
                return instance;
            }
        }
        public virtual void Awake()
        {
            if (instance == null)
                instance = gameObject.GetComponent<T>();
            else
                Destroy(gameObject);
        }
    }
}
