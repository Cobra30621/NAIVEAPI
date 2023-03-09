using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

namespace NaiveAPI
{
    namespace MathRelated
    {
        public class TwoBoneIK : MonoBehaviour
        {
            public Transform Begin;
            public Transform Mid;
            public Transform End;
            public Transform Target;
            public Transform Pole;
            public float UpperRotation;
            public float LowerRotation;

            private float LowerLength;
            private float EndLength;
            private float UpperToTarget;
            private Vector3 en;
            public void Update()
            {
                LowerLength = Mid.localPosition.magnitude;
                EndLength = End.localPosition.magnitude;
                UpperToTarget = Vector3.Distance(Begin.position, Target.position);
                en = Vector3.Cross(Target.position - Begin.position, Pole.position - Begin.position);

                Begin.rotation = Quaternion.LookRotation(Target.position - Begin.position, Quaternion.AngleAxis(UpperRotation, Mid.position - Begin.position) * (en));
                Begin.rotation *= Quaternion.Inverse(Quaternion.FromToRotation(Vector3.forward, Mid.localPosition));
                Begin.rotation = Quaternion.AngleAxis(-calAngle(), -en) * Begin.rotation;

                Mid.rotation = Quaternion.LookRotation(Target.position - Mid.position, Quaternion.AngleAxis(LowerRotation, End.position - Mid.position) * (en));
                Mid.rotation *= Quaternion.Inverse(Quaternion.FromToRotation(Vector3.forward, End.localPosition));
            }
            private float calAngle()
            {
                if (!float.IsNaN(Acos((-(EndLength * EndLength) + (LowerLength * LowerLength) + (UpperToTarget * UpperToTarget)) / (-2 * LowerLength * UpperToTarget)) * Rad2Deg))
                {
                    return Acos((-(EndLength * EndLength) + (LowerLength * LowerLength) + (UpperToTarget * UpperToTarget)) / (2 * LowerLength * UpperToTarget)) * Rad2Deg;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}