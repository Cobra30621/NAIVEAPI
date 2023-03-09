using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    namespace MathRelated
    {
        [System.Serializable]
        public struct Angle
        {
            [SerializeField]
            private float angle;

            public Angle(float degree)
            {
                angle = 0;
                Degree = degree;
            }

            public float Degree
            {
                get
                {
                    return angle;
                }

                set
                {
                    if (value > 180)
                        angle = -180 + value % 180;
                    else if (value < -180)
                        angle = 180 + value % 180;
                    else
                        angle = value;
                }
            }

            public float Degree360
            {
                get
                {
                    return angle + (angle > 0 ? 180 : 360);
                }
            }

            public float Radians
            {
                get
                {
                    return angle * Mathf.Deg2Rad;
                }
                set
                {
                    angle = value * Mathf.Rad2Deg;
                }
            }

            public Vector2 Vector
            {
                get
                {
                    return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
                }
                set
                {
                    Radians = Mathf.Atan2(value.y, value.x);
                }
            }

            public static Angle Position2Angle(GameObject obj1, GameObject obj2)
            {
                return Position2Angle(obj1.transform.position, obj2.transform.position);
            }
            public static Angle Position2Angle(GameObject obj, Vector2 point)
            {
                return Position2Angle(obj.transform.position, point);
            }
            public static Angle Position2Angle(Vector2 point, GameObject obj)
            {
                return Position2Angle(point, obj.transform.position);
            }
            public static Angle Position2Angle(Vector2 point1, Vector2 point2)
            {
                Angle output = new Angle();
                output.Vector = point2 - point1;
                return output;
            }

            public static Angle operator +(Angle angle, Angle angle1)
            {
                return angle + angle1.Degree;
            }

            public static Angle operator +(Angle angle, float value)
            {
                return new Angle(angle.Degree + value);
            }

            public static Angle operator -(Angle angle, Angle angle1)
            {
                return angle - angle1.Degree;
            }

            public static Angle operator -(Angle angle, float value)
            {
                return new Angle(angle.Degree - value);
            }

            public static Angle operator *(Angle angle, Angle angle1)
            {
                return angle * angle1.Degree;
            }

            public static Angle operator *(Angle angle, float value)
            {
                return new Angle(angle.Degree * value);
            }

            public static Angle operator /(Angle angle, Angle angle1)
            {
                return angle / angle1.Degree;
            }

            public static Angle operator /(Angle angle, float value)
            {
                return new Angle(angle.Degree / value);
            }

            public static bool operator ==(Angle angle, Angle angle1)
            {
                return angle == angle1.Degree;
            }

            public static bool operator ==(Angle angle, float value)
            {
                return angle.Degree == value;
            }

            public static bool operator !=(Angle angle, Angle angle1)
            {
                return angle != angle1.Degree;
            }

            public static bool operator !=(Angle angle, float value)
            {
                return angle.Degree != value;
            }

            public static implicit operator float(Angle angle)
            {
                return angle.Degree;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return angle == ((Angle)obj).Degree;
            }
        }
    }
}
