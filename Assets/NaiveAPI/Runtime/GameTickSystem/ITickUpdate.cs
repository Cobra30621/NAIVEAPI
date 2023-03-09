namespace NaiveAPI
{
    namespace GameTickSystem
    {
        public interface ITickUpdate
        {
            TickUpdateInfo updateInfo { get; set; }
            public void TickUpdate();
            /// <summary>
            /// update every "UpdateFrequency" TickRate
            /// </summary>
            public void Start(int frequence = 1)
            {
                if(updateInfo.UpdateID != -1) Close();
                updateInfo.UpdateFrequency = frequence;
                GameTick.Subscribe(this);
            }

            public void Close()
            {
                GameTick.UnSubscribe(this);
            }
        }


    }
}