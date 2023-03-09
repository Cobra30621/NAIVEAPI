using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;


namespace NaiveAPI
{
    namespace MathRelated
    {
        [System.Serializable]
        public class SecondOrderController
        {
            [SerializeField]
            private float y, yd, lastY, lastX;
            [SerializeField]
            private float twoPiF, d, f = 0.05f, z = 0.5f, r = 0, k1, k2, k3, spd = 1;

            /// <summary>
            /// same with return of Controller.Update()
            /// </summary>
            public float NowY { get => y; }
            public float Velocity { get => yd; }
            public float Target { get => lastX; }
            public float F { get => f; }
            public float Z { get => z; }
            public float R { get => r; }
            public float Spd { get => spd; }
            public float TwoPiF { get => twoPiF; }
            public float D { get => d; }
            public float K1 { get => k1; }
            public float K2 { get => k2; }
            public float K3 { get => k3; }

            public SecondOrderController()
            {
                Init(0.05f, 0.5f, 0, 0);
            }
            public SecondOrderController(float f, float z, float r, float x0 ,float spd = 1)
            {
                Init(f, z, r, x0, spd);
            }
            public void Init(float f, float z, float r, float x0, float spd = 1f)
            {
                this.f = f;
                this.z = z;
                this.r = r;

                twoPiF = 2 * PI * f;
                d = twoPiF * Sqrt(Abs(z * z - 1));
                k1 = z / (PI * f);
                k2 = 1f / (twoPiF * twoPiF);
                k3 = r * z / twoPiF;

                lastX = x0;
                y = x0;
                yd = 0;
                this.spd = spd;
            }
            public float Update(float targetValue, float spd)
            {
                this.spd = spd;
                return Update(targetValue);
            }
            public float Update(float targetValue)
            {
                float nowD = y - lastY;
                if (nowD == 0 || targetValue != lastX)
                {
                    nowD = (targetValue - lastX) / spd;
                    lastX = targetValue;
                }
                lastY = y;
                y = y + spd * yd;
                yd = yd + spd * (targetValue + k3 * nowD - y - k1 * yd) / k2;

                return y;
            }

            /// <summary>
            /// Calculate and return the graph of this controller's behaviour.
            /// </summary>
            public Texture2D GetGraph(Vector2Int textureSize)
            {
                Color backGroundColor = new Color(.2f, .2f, .2f);
                Color xColor = new Color(.6f, .6f, .8f);
                Color yColor = new Color(.6f, .8f, .6f);
                Texture2D graph = new Texture2D(textureSize.x, textureSize.y);
                SecondOrderController controller = new SecondOrderController(f, z, r, 0, spd);

                float[] yPoints = new float[300];
                float[] xPoints = new float[300];
                float max = -1000;
                float min = 1000;
                for (int i = 0; i < textureSize.x; i++)
                {
                    for (int j = 0; j < textureSize.y; j++)
                    {
                        graph.SetPixel(i, j, backGroundColor);
                    }
                }
                for (int i = 0; i < 50; i++)  // target 0
                {
                    xPoints[i] = 0;
                }
                for (int i = 50; i < 150; i++)// target 50
                {
                    xPoints[i] = 50;
                }
                for (int i = 150; i < 250; i++)// target line
                {
                    xPoints[i] = 125 - i / 2f;
                }
                for (int i = 250; i < 300; i++)// 
                {
                    xPoints[i] = 0;
                }
                for (int i = 0; i < 300; i++)
                {
                    yPoints[i] = controller.Update(xPoints[i]);
                    if (yPoints[i] > max) max = yPoints[i];
                    if (yPoints[i] < min) min = yPoints[i];
                }

                for (int i = 0; i < 300; i++) // calculate fixed position
                {
                    yPoints[i] = textureSize.y * ((yPoints[i] - min) / (max - min)) * .7f + textureSize.y * .15f;
                    xPoints[i] = textureSize.y * ((xPoints[i] - min) / (max - min)) * .7f + textureSize.y * .15f;
                }
                float lastYPos = xPoints[49];
                for (float j = lastYPos; Abs(j - xPoints[50]) > 1; j += lastYPos > xPoints[50] ? -1f : 1f)
                {
                    graph.SetPixel((int)(textureSize.x * (50 / 300f)), (int)j, xColor);
                    graph.SetPixel((int)(textureSize.x * (50 / 300f)) + 1, (int)j, xColor);
                    graph.SetPixel((int)(textureSize.x * (50 / 300f)), (int)j + 1, xColor);
                    graph.SetPixel((int)(textureSize.x * (50 / 300f)) + 1, (int)j + 1, xColor);
                }
                lastYPos = yPoints[0];
                for (int i = 1; i < textureSize.x - 2; i++)
                {
                    int check = 0;
                    int xPos = (int)(300f * (i / (float)textureSize.x));
                    graph.SetPixel(i + 1, (int)xPoints[xPos], xColor);
                    graph.SetPixel(i + 1, (int)xPoints[xPos] + 1, xColor);
                    graph.SetPixel(i + 2, (int)xPoints[xPos], xColor);
                    graph.SetPixel(i + 2, (int)xPoints[xPos] + 1, xColor);

                    graph.SetPixel(i + 1, (int)(yPoints[xPos] - .5f), yColor);
                    graph.SetPixel(i + 1, (int)(yPoints[xPos] + .5f), yColor);
                    graph.SetPixel(i + 2, (int)(yPoints[xPos] - .5f), yColor);
                    graph.SetPixel(i + 2, (int)(yPoints[xPos] + .5f), yColor);
                    for (float j = lastYPos; Abs(j - yPoints[xPos]) > 1; j += lastYPos > yPoints[xPos] ? -1f : 1f)
                    {
                        graph.SetPixel(i, (int)j, yColor);
                        graph.SetPixel(i, (int)j + 1, yColor);
                        graph.SetPixel(i + 1, (int)j, yColor);
                        graph.SetPixel(i + 1, (int)j + 1, yColor);
                        check++;
                        if (check > 299)
                            break;
                    }
                    lastYPos = yPoints[xPos];
                }

                graph.filterMode = FilterMode.Point;
                graph.Apply();
                return graph;
            }

            public SecondOrderController Copy()
            {
                return new SecondOrderController(f, z, r, 0, spd);
            }
        }
    }
}
