using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI {
    namespace MathRelated {
        [System.Serializable]
        public class Movement
        {
            #region member variable

            public Vector3 Acceleration;
            public Vector3 Velocity;             
            public float MaxSpeed;
            public float VelocityDecrease;       //  velocity -= Friction
            public float VelocityDecreaseRate;   //  velocity *= (Friction/100)
            public Transform Target;

            #endregion

            public void Update()
            {
                Velocity += Acceleration;
                float temp =Vector3.Magnitude(Velocity);
                temp *= (1-VelocityDecreaseRate);
                temp -= VelocityDecrease;
                if (temp < 0)
                {
                    temp = 0;
                }
                else if (temp > MaxSpeed)
                {
                    temp = MaxSpeed;
                }
                Velocity = Velocity.normalized * temp;
                Target.position += Velocity;
            }


        }
    }
}

