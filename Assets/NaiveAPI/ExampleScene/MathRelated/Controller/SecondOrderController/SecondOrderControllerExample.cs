using NaiveAPI.GameTickSystem;
using NaiveAPI.MathRelated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    namespace Example
    {
        public class SecondOrderControllerExample : MonoBehaviour, ITickUpdate
        {
            [SerializeField] SecondOrderController controller;
            SecondOrderController[] posController;

            [SerializeField] Transform target;

            public TickUpdateInfo updateInfo { get; set; }

            private void Start()
            {
                posController = new SecondOrderController[3];
                for (int i = 0; i < 3; i++) 
                {
                    posController[i] = controller.Copy();
                }

                (this as ITickUpdate).Start();
            }
            public void TickUpdate()
            {
                Vector3 newPosition;
                newPosition.x = posController[0].Update(target.position.x);
                newPosition.y = posController[1].Update(target.position.y);
                newPosition.z = posController[2].Update(target.position.z);
                transform.position = newPosition;
            }
        }
    }
}
