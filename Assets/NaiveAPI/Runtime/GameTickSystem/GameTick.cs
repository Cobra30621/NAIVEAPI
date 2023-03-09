using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    namespace GameTickSystem
    {
        public class GameTick : MonoSingleton<GameTick>
        {
            #region Declare Value
            [SerializeField]
            private int tickPerSec = 60;
            [SerializeField]
            private int tickRate = 60;
            [SerializeField]
            private int currentTick;
            [SerializeField]
            private int currentRealTick;
            [SerializeField]
            private float currentTime;
            private float deltaTime;
            private float deltaTimeTick;
            private float lastTickTime;
            private float lastTickRateTime;
            private float tick2sec;
            private float tickRate2sec;


            public List<TickRateUpdateInfo> TickUpdatesList = new List<TickRateUpdateInfo>();

            #endregion

            #region Get Set
            public static void Initialize() { Instance.init(); }
            public static void Subscribe(ITickUpdate tickUpdate) { Instance.subscribe(tickUpdate); }
            public static void UnSubscribe(ITickUpdate tickUpdate) { Instance.unSubscribe(tickUpdate); }
            public static int TickPerSec
            {
                get
                {
                    return instance.tickPerSec;
                }
            }
            public static int TickRate
            {
                get
                {
                    return instance.tickRate;
                }
                set
                {
                    instance.tickRate = value;
                    instance.tickRate2sec = 1f / instance.tickRate;
                    instance.lastTickRateTime = instance.currentTime;
                }
            }
            public static float GameSpeed
            {
                get
                {
                    return instance.tickRate / instance.tickPerSec;
                }
                set
                {
                    TickRate = (int)(instance.tickPerSec * value);
                }
            }
            public static int CurrentTick { get { return instance.currentTick; } }
            public static int CurrentRealTick { get { return instance.currentRealTick; } }
            public static float CurrentTime { get { return instance.currentTime; } }
            public static float DeltaTime { get { return instance.deltaTime; } }
            public static float DeltaTickTime { get { return instance.deltaTimeTick; } }
            #endregion
            override public void Awake()
            {
                base.Awake();
                init();
            }
            void Update()
            {
                deltaTime = Time.deltaTime;
                deltaTimeTick += deltaTime;
                currentTime += deltaTime;

                getKeyTickTime();
                doTickupdate();
            }
            private void doTickupdate()
            {
                // real tick counter
                if ((int)(currentTime / tick2sec) > currentRealTick)
                {
                    caculateRealTickGetKeyValue();
                    // real tick Update
                    for (int i = TickUpdatesList[0].Count - 1; i >= 0; i--)
                        TickUpdatesList[0].UpdatesList[i].TickUpdate();

                    currentRealTick++;
                }

                // tick rate counter
                if (currentTime-lastTickRateTime > tickRate2sec)
                {
                    lastTickRateTime += tickRate2sec;
                    caculateTickGetKeyValue();

                    // TickRate Update
                    for (int i = TickUpdatesList[1].Count -1; i>=0; i--)
                        TickUpdatesList[1].UpdatesList[i].TickUpdate();

                    // Special TickRate Update
                    for (int i = 2; i < TickUpdatesList.Count; i++)
                    {
                        TickRateUpdateInfo specialTickUpdate = TickUpdatesList[i];
                        if (currentTick % specialTickUpdate.UpdateFrequency == 0)
                        {
                            caculateSpecialTickGetKeyValue(specialTickUpdate.UpdateFrequency);
                            List<ITickUpdate> tickUpdates = specialTickUpdate.UpdatesList;
                            for (int j = tickUpdates.Count-1; j >=0 ; j--)
                                tickUpdates[j].TickUpdate();
                        }
                    }
                    currentTick++;
                    deltaTimeTick = currentTime - lastTickTime;
                    lastTickTime = currentTime;
                }
            }

            private void init()
            {
                currentTick = 0;
                currentRealTick = 0;
                currentTime = 0;
                tick2sec = 1f / tickPerSec;
                tickRate2sec = 1f / tickRate;
                lastTickTime = 0;
                TickUpdatesList.Clear();
                TickUpdatesList.Clear();
                TickUpdatesList.Add(new TickRateUpdateInfo()); // real tick update
                TickUpdatesList.Add(new TickRateUpdateInfo()); // frequence 1 update
            }

            private void subscribe(ITickUpdate tickUpdate)
            {
                int index = tickUpdate.updateInfo.UpdateFrequency;
                index = getTickUpdatesListIndex(tickUpdate.updateInfo);
                if (index == -1 && tickUpdate.updateInfo.UpdateFrequency > 1)
                {
                    TickUpdatesList.Add(new TickRateUpdateInfo());
                    index = TickUpdatesList.Count - 1;
                    TickUpdatesList[index].UpdateFrequency = tickUpdate.updateInfo.UpdateFrequency;
                }
                tickUpdate.updateInfo.UpdateID = TickUpdatesList[index].Count;
                TickUpdatesList[index].UpdatesList.Add(tickUpdate);
                TickUpdatesList[index].Count++;
            }
            private void unSubscribe(ITickUpdate tickUpdate)
            {
                int index = getTickUpdatesListIndex(tickUpdate.updateInfo);
                if (index < 0)
                    return;

                TickUpdatesList[index].Count--;
                TickUpdatesList[index].UpdatesList[tickUpdate.updateInfo.UpdateID] = TickUpdatesList[index].UpdatesList[TickUpdatesList[index].Count];
                TickUpdatesList[index].UpdatesList.RemoveAt(TickUpdatesList[index].Count);
                tickUpdate.updateInfo.UpdateID = -1;
            }

            private int getTickUpdatesListIndex(TickUpdateInfo info)
            {
                if (info.UpdateFrequency < 2)
                    return info.UpdateFrequency;
                for (int i = TickUpdatesList.Count - 1; i >= 2; i--)
                {
                    if (TickUpdatesList[i].UpdateFrequency == info.UpdateFrequency)
                    {
                        return i;
                    }
                }
                return -1;
            }

            #region GetKey Caculate
            public class TickGetKeyInfo
            {
                public TickGetKeyInfo(KeyCode keyCode)
                {
                    this.KeyCode = keyCode;
                    this.DownTick = -1;
                    this.UpTick = -1;
                    this.DownRealTick = -1;
                    this.UpRealTick = -1;
                }

                public KeyCode KeyCode;

                public bool GetKeyDown;
                public bool GetKeyUp;
                public bool GetKey;

                public int DownTick;
                public int UpTick;
                public int DownRealTick;
                public int UpRealTick;
            }
            private Dictionary<KeyCode, TickGetKeyInfo> tickGetKeyInfo = new Dictionary<KeyCode, TickGetKeyInfo>();
            private void getKeyTickTime()
            {
                foreach (TickGetKeyInfo info in tickGetKeyInfo.Values)
                {
                    if (Input.GetKeyDown(info.KeyCode))
                    {
                        info.DownRealTick = currentRealTick;
                        info.DownTick = currentTick;
                    }
                    if (Input.GetKeyUp(info.KeyCode))
                    {
                        info.UpRealTick = currentRealTick;
                        info.UpTick = currentTick;
                    }
                }
            }
            private void caculateRealTickGetKeyValue()
            {
                foreach (TickGetKeyInfo info in tickGetKeyInfo.Values)
                {
                    info.GetKeyDown = info.DownRealTick == currentRealTick;
                    info.GetKeyUp = info.UpRealTick == currentRealTick;

                    if (info.GetKeyDown)
                        info.GetKey = true;
                    if (info.GetKeyUp)
                        info.GetKey = false;
                }
            }
            private void caculateTickGetKeyValue()
            {
                foreach (TickGetKeyInfo info in tickGetKeyInfo.Values)
                {
                    info.GetKeyDown = info.DownTick == currentTick;
                    info.GetKeyUp = info.UpTick == currentTick;

                    if (info.GetKeyDown)
                        info.GetKey = true;
                    if (info.GetKeyUp)
                        info.GetKey = false;
                }
            }
            private void caculateSpecialTickGetKeyValue(int updateFrequency)
            {
                foreach (TickGetKeyInfo info in tickGetKeyInfo.Values)
                {
                    info.GetKeyDown = info.DownTick > 0 && ((currentTick - updateFrequency) == (info.DownTick - 1 - ((info.DownTick - 1) % updateFrequency)));
                    info.GetKeyUp = info.UpTick > 0 && ((currentTick - updateFrequency) == (info.UpTick - 1 - ((info.UpTick - 1) % updateFrequency)));
                    if (info.GetKeyDown)
                        info.GetKey = true;
                    if (info.GetKeyUp)
                        info.GetKey = false;
                }
            }
            public static bool GetKey(KeyCode keyCode)
            {
                TickGetKeyInfo output;
                if (!instance.tickGetKeyInfo.TryGetValue(keyCode, out output))
                {
                    instance.tickGetKeyInfo.Add(keyCode, new TickGetKeyInfo(keyCode));
                    output = instance.tickGetKeyInfo[keyCode];
                }
                return output.GetKey;
            }
            public static bool GetKeyDown(KeyCode keyCode)
            {
                TickGetKeyInfo output;
                if (!instance.tickGetKeyInfo.TryGetValue(keyCode, out output))
                {
                    instance.tickGetKeyInfo.Add(keyCode, new TickGetKeyInfo(keyCode));
                    output = instance.tickGetKeyInfo[keyCode];
                }
                return output.GetKeyDown;
            }
            public static bool GetKeyUp(KeyCode keyCode)
            {
                TickGetKeyInfo output;
                if (!instance.tickGetKeyInfo.TryGetValue(keyCode, out output))
                {
                    instance.tickGetKeyInfo.Add(keyCode, new TickGetKeyInfo(keyCode));
                    output = instance.tickGetKeyInfo[keyCode];
                }
                return output.GetKeyUp;
            }
            #endregion

            public class TickRateUpdateInfo
            {
                public int UpdateFrequency = 1;
                public int Count = 0;
                public List<ITickUpdate> UpdatesList = new List<ITickUpdate>();
            }
        }
    }
}
