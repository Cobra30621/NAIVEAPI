using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    namespace MathRelated
    {
        public class EulerAngle
        {
            private Angle[] angles = new Angle[3] { new Angle(), new Angle(), new Angle() };

            public EulerAngle()
            {
                angles[0].Degree = 0;
                angles[1].Degree = 0;
                angles[2].Degree = 0;
            }

            public EulerAngle(Angle x, Angle y, Angle z)
            {
                angles[0] = x;
                angles[1] = y;
                angles[2] = z;
            }

            public Angle x
            {
                get
                {
                    return angles[0];
                }
                set
                {
                    angles[0] = value;
                }
            }

            public Angle y
            {
                get
                {
                    return angles[1];
                }
                set
                {
                    angles[1] = value;
                }
            }

            public Angle z
            {
                get
                {
                    return angles[2];
                }
                set
                {
                    angles[2] = value;
                }
            }

            public Vector3 EulerVector
            {
                get
                {
                    return new Vector3(angles[0].Degree, angles[1].Degree, angles[2].Degree);
                }

                set
                {
                    angles[0].Degree = value.x;
                    angles[1].Degree = value.y;
                    angles[2].Degree = value.z;
                }
            }

            public Quaternion ToQuaternion
            {
                get
                {
                    return Quaternion.Euler(EulerVector);
                }
            }

            public void FromPosition(Vector3 point1, Vector3 point2)
            {
                Vector3 vector = point2 - point1;
                EulerVector = Quaternion.LookRotation(vector, Vector3.up).eulerAngles;
            }

            public override string ToString()
            {
                return EulerVector.ToString();
            }

            public static EulerAngle Position2Angle(Vector3 point1, Vector3 point2)
            {
                EulerAngle eulerAngle = new EulerAngle();
                Vector3 vector = point2 - point1;
                eulerAngle.EulerVector = Quaternion.LookRotation(vector, Vector3.up).eulerAngles;

                return eulerAngle;
            }

            public static EulerAngle operator +(EulerAngle left, EulerAngle right)
            {
                return new EulerAngle(left.x + right.x, left.y + right.y, left.z + right.z);
            }

            public static EulerAngle operator -(EulerAngle left, EulerAngle right)
            {
                return new EulerAngle(left.x - right.x, left.y - right.y, left.z - right.z);
            }

            public static EulerAngle operator *(EulerAngle left, EulerAngle right)
            {
                return new EulerAngle(left.x * right.x, left.y * right.y, left.z * right.z);
            }

            public static EulerAngle operator /(EulerAngle left, EulerAngle right)
            {
                return new EulerAngle(left.x / right.x, left.y / right.y, left.z / right.z);
            }

            public static bool operator ==(EulerAngle left, EulerAngle right)
            {
                return left.x == right.x && left.y == right.y && left.z == right.z;
            }

            public static bool operator !=(EulerAngle left, EulerAngle right)
            {
                return !(left == right);
            }

            public override bool Equals(object obj)
            {
                return this == (EulerAngle)obj;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
    }
}


