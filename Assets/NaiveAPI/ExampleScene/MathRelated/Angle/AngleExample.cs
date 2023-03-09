using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaiveAPI.MathRelated;

namespace NaiveAPI
{
    namespace Example
    {
        public class AngleExample : MonoBehaviour
        {
            [Header("ForSpinGameObjectExample")]
            public GameObject SpinGameObjectExample;
            public Angle angle;
            [Header("ForPosition2AngleExample")]
            public GameObject Position2AngleExample;
            public GameObject Cube;
            public Vector3 startPoint;
            public Angle angle1;

            // Start is called before the first frame update
            void Start()
            {
            }

            // Update is called once per frame
            void Update()
            {
                EulerAngle eulerAngle = EulerAngle.Position2Angle(Cube.transform.position, Position2AngleExample.transform.position);
                Cube.transform.rotation = Quaternion.Euler(eulerAngle.EulerVector);

                /*
                Debug.Log(Cube.transform.rotation.eulerAngles);
                angle.SpinGameObject(SpinGameObjectExample.transform, 1);
                angle1 = Angle.Position2Angle(startPoint, Position2AngleExample);
                print(angle1.Degree);
                angle1.SpinGameObject(Cube.transform, 1);
                */
            }

            private void OnDrawGizmos()
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(Cube.transform.position, Position2AngleExample.transform.position);
                /*
                Gizmos.color = Color.red;
                Gizmos.DrawLine(startPoint, Position2AngleExample.transform.position);
                Gizmos.DrawLine(startPoint, new Vector3(Position2AngleExample.transform.position.x, 0, Position2AngleExample.transform.position.z));
                angle1.DrawGizmos();
                */
            }
        }
    }

}
