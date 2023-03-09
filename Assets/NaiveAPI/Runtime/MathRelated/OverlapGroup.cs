using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NaiveAPI
{
    namespace MathRelated
    {
        [System.Serializable]
        public class OverlapGroup
        {
            public List<CubeData> CubeDatas = new List<CubeData>();
            public List<SphereData> SphereDatas = new List<SphereData>();

            /// <summary>
            /// this function can only be used in OnDrawGizmos and OnDrawGizmosSelected
            /// </summary>
            public void DrawGizmos()
            {
                Gizmos.color = new Color(0.8f, 1, 0.8f, 0.3f);

                for (int i = 0; i < CubeDatas.Count; i++)
                {
                    Gizmos.matrix = Matrix4x4.TRS(CubeDatas[i].Center, Quaternion.Euler(CubeDatas[i].Orientation), Gizmos.matrix.lossyScale);
                    Gizmos.DrawCube(Vector3.zero, 2 * CubeDatas[i].HalfExtents);
                }

                Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Gizmos.matrix.lossyScale);
                for (int i = 0; i < SphereDatas.Count; i++)
                {
                    Gizmos.DrawSphere(SphereDatas[i].Position, SphereDatas[i].Radius);
                }
            }

            public Collider GetNearestCollider(Vector3 position)
            {
                Collider[] colliders = GetColliders();
                float nearestDistance = Vector3.Distance(position, colliders[0].transform.position);
                float currentDistance;
                int current = 0;
                for (int i = 1; i < colliders.Length; i++)
                {
                    currentDistance = Vector3.Distance(position, colliders[i].transform.position);
                    if (nearestDistance > currentDistance)
                    {
                        nearestDistance = currentDistance;
                        current = i;
                    }
                }

                return colliders[current];
            }

            public Collider GetFarthestCollider(Vector3 position)
            {
                Collider[] colliders = GetColliders();
                float farthestDistance = Vector3.Distance(position, colliders[0].transform.position);
                float currentDistance;
                int current = 0;
                for (int i = 1; i < colliders.Length; i++)
                {
                    currentDistance = Vector3.Distance(position, colliders[i].transform.position);
                    if (farthestDistance < currentDistance)
                    {
                        farthestDistance = currentDistance;
                        current = i;
                    }
                }

                return colliders[current];
            }

            public Collider GetRandomCollider()
            {
                Collider[] colliders = GetColliders();
                return colliders[Random.Range(0, colliders.Length)];
            }

            public Collider[] GetColliders()
            {
                List<Collider> colliders = new List<Collider>();
                Collider[] colliderArray;
                int Count = CubeDatas.Count;

                for (int i = 0; i < Count; i++)
                {
                    colliderArray = Physics.OverlapBox(CubeDatas[i].Center, CubeDatas[i].HalfExtents, Quaternion.Euler(CubeDatas[i].Orientation), CubeDatas[i].LayerMask);
                    for (int j = 0; j < colliderArray.Length; j++)
                    {
                        if (!colliders.Contains(colliderArray[j]))
                            colliders.Add(colliderArray[j]);
                    }
                }

                Count = SphereDatas.Count;

                for (int i = 0; i < Count; i++)
                {
                    colliderArray = Physics.OverlapSphere(SphereDatas[i].Position, SphereDatas[i].Radius, SphereDatas[i].LayerMask);
                    for (int j = 0; j < colliderArray.Length; j++)
                    {
                        if (!colliders.Contains(colliderArray[j]))
                            colliders.Add(colliderArray[j]);
                    }
                }

                return colliders.ToArray();
            } 

            [System.Serializable]
            public class CubeData
            {
                public Vector3 Center;
                public Vector3 HalfExtents;
                public Vector3 Orientation;
                public LayerMask LayerMask;
            }
            [System.Serializable]
            public class SphereData
            {
                public Vector3 Position;
                public float Radius;
                public LayerMask LayerMask;
            }
        }
    }
}

