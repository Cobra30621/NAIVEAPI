using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaiveAPI.MathRelated;
using NaiveAPI.GameTickSystem;

namespace NaiveAPI
{
    namespace Example
    {
        public class CreatureExample : MonoBehaviour,ITickUpdate
        {
            [SerializeField] Transform   Core; // creature's movement core
            [SerializeField] Transform   Body; // Body collider object
            [SerializeField] Transform[] LegTargets;                // IK's calculate position
            [SerializeField] Vector3[] footHold = new Vector3[4];   // RightFront, RightBack, LeftFront, LeftBack
            [SerializeField] Vector3[] targetHold = new Vector3[4]; // { RF, RB, LF, LB }
            [SerializeField] SecondOrderController LegControl;      // for inspector controller setting

            SecondOrderController[] controller = new SecondOrderController[15];// { RF, RB, LF, LB, BodyRotate }*3    (x,y,z)
            EulerAngle bodyRotation = new EulerAngle();  // for different ground height
            private bool hasGround = true;               // check if all foot has ground to locate
            private bool[] isFootOnGround = new bool[4]; // { RF, RB, LF, LB }
            private Vector2 velocity;            // movement's velocity
            private Vector3 lastPosition;
            [SerializeField] float stepDistance; // how far shoold foot move

            public TickUpdateInfo updateInfo { get ; set; }

            private void Start()
            {
                (this as ITickUpdate).Start();
                lastPosition = Body.position;
                bodyRotation.y = new Angle(180);
                for (int i = 0; i < 4; i++)
                    targetHold[i] = LegTargets[i].position;
                for (int i = 0; i < 15; i++)
                    controller[i] = LegControl.Copy(); // init all controller
            }
            public void TickUpdate()
            {
                #region Movement Control
                Vector3 corePos = Core.position;
                Vector2 movement = Vector2.zero;
                if (GameTick.GetKey(KeyCode.W))
                    movement.y = 1;
                if (GameTick.GetKey(KeyCode.S))
                    movement.y = -1;
                if (GameTick.GetKey(KeyCode.D))
                    movement.x = 1;
                if (GameTick.GetKey(KeyCode.A))
                    movement.x = -1;

                movement.Normalize();
                movement *= .2f;
                corePos.x += movement.x;
                corePos.z += movement.y;
                Core.position = corePos;
                #endregion

                #region Body Rotation
                hasGround = true;
                // find footHold position an set it at nearest ground
                RaycastHit hit = new RaycastHit();
                for (int i = 0; i < 4; i++)
                {
                    footHold[i] = new Vector3(Body.position.x + (i < 2 ? 1.5f : -1.5f), Body.position.y+10, Body.position.z + ((i==0 || i==3) ? 1.5f : -1.5f));
                    if (Physics.Raycast(new Ray(footHold[i], Vector3.down), out hit))
                        footHold[i].y -= hit.distance;
                    else
                    {
                        footHold[i].y -= 15;
                        hasGround = false;
                    }
                }

                // calculate body rotation
                Vector2 point1, point2;
                Angle rotaAngle;
                point1 = new Vector2((footHold[0].z + footHold[3].z) / 2f, (footHold[0].y + footHold[3].y) / 2f);
                point2 = new Vector2((footHold[1].z + footHold[2].z) / 2f, (footHold[1].y + footHold[2].y) / 2f);
                rotaAngle = Angle.Position2Angle(point1, point2);
                bodyRotation.x = rotaAngle;
                point1 = new Vector2((footHold[0].x + footHold[1].x) / 2f, (footHold[0].y + footHold[1].y) / 2f);
                point2 = new Vector2((footHold[2].x + footHold[3].x) / 2f, (footHold[2].y + footHold[3].y) / 2f);
                rotaAngle = Angle.Position2Angle(point1, point2);
                bodyRotation.z = rotaAngle;
                Body.rotation = Quaternion.Euler(bodyRotation.EulerVector);

                // check ground
                if (hasGround)
                {
                    Body.position = new Vector3(Body.position.x,((footHold[0].y+ footHold[1].y+ footHold[2].y+ footHold[3].y)/4f) + 1.5f, Body.position.z);
                }
                #endregion

                #region Legs Move
                // check if foot is on the ground
                // - foot can move when at least 2 foots on ground
                for (int i = 0; i < 4; i++)
                {
                    isFootOnGround[i] = Vector3.Distance(targetHold[i], LegTargets[i].position) < .5f;
                }

                // calculate velocity and give Max Min limit
                velocity = new Vector2(Body.position.x - lastPosition.x, Body.position.z - lastPosition.z);
                velocity.x = Mathf.Clamp(velocity.x, -.1f, .1f);
                velocity.y = Mathf.Clamp(velocity.y, -.1f, .1f);

                // find new targetHold location
                Vector3 offset = new Vector3(velocity.x * 4, 0, velocity.y * 4);
                if (Vector3.Distance(targetHold[0], footHold[0] + offset) > stepDistance ||
                    Vector3.Distance(targetHold[2], footHold[2] + offset) > stepDistance)
                {

                    if(isFootOnGround[1] && isFootOnGround[3])
                    {
                        targetHold[0] = new Vector3(footHold[0].x + velocity.x * 10, footHold[0].y, footHold[0].z + velocity.y * 10);
                        targetHold[2] = new Vector3(footHold[2].x + velocity.x * 10, footHold[2].y, footHold[2].z + velocity.y * 10);
                    }
                }
                else if(Vector3.Distance(targetHold[1], footHold[1] + offset) > stepDistance ||
                        Vector3.Distance(targetHold[3], footHold[3] + offset) > stepDistance)
                {

                    if (isFootOnGround[0] && isFootOnGround[2])
                    {
                        targetHold[1] = new Vector3(footHold[1].x + velocity.x * 10, footHold[1].y, footHold[1].z + velocity.y * 10);
                        targetHold[3] = new Vector3(footHold[3].x + velocity.x * 10, footHold[3].y, footHold[3].z + velocity.y * 10);
                    }
                }

                // set new targetHold location's height
                for (int i = 0; i < 4; i++)
                {
                    targetHold[i].y += 10;
                    if (Physics.Raycast(new Ray(targetHold[i], Vector3.down), out hit))
                    {
                        targetHold[i].y -= hit.distance;
                        Vector2 legPos = new Vector2(LegTargets[i].position.x, LegTargets[i].position.z);
                        float distance = Mathf.Min(Vector2.Distance(legPos, new Vector2(targetHold[i].x, targetHold[i].z)),
                                                   Vector2.Distance(legPos, new Vector3(footHold[i].x, footHold[i].z)));
                        if (Mathf.Abs(distance) > .25f)
                            targetHold[i].y += (distance*1.5f) - .2f;
                    }
                    else
                    {
                        targetHold[i].y -= 15;
                        hasGround = false;
                    }
                }

                // apply targetHold to SecondOrderController for animation
                for (int i = 0; i < 12; i += 3)
                {
                    Vector3 pos = targetHold[i / 3];
                    LegTargets[i / 3].position = new Vector3(
                        controller[i].Update(pos.x),
                        controller[i + 1].Update(pos.y),
                        controller[i + 2].Update(pos.z));
                }
                lastPosition = Body.position;
                #endregion
            }

            // debug
            private void OnDrawGizmos()
            {
                if (!Application.isPlaying)
                    return;
                Gizmos.color = new Color(1f, .5f, .5f, .7f);
                foreach (var pos in footHold)
                    Gizmos.DrawSphere(pos, .2f);
                Gizmos.color = new Color(.5f, .5f, 1f, .7f);
                foreach (var pos in targetHold)
                    Gizmos.DrawSphere(pos, .2f);
            }
        }
    }
}
