using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    namespace MathRelated
    {
        [System.Serializable]
        public class PIDController
        {
            public float P;
            public float I;
            public float D;

            public Action<float> OnUpdated;

            private float error;
            private float lastError;
            private float accumulationError;
            private float fixValue;
            /// <summary>
            /// Asign new valie
            /// </summary>
            public float Error
            {
                get
                {
                    return error;
                }
                set
                {
                    error = value;
                    accumulationError += error;
                    fixValue = (error * P) + ((lastError - error) * D) + (accumulationError * I);
                    OnUpdated?.Invoke(fixValue);
                    lastError = error;
                }
            }
            public float LastError { get => lastError; }
            public float AccumulationError { get => accumulationError; }
            public float FixValue { get => fixValue; }
        }
    }
}
