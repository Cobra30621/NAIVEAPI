using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaiveAPI.GameTickSystem;
public class EmptyTick : MonoBehaviour,ITickUpdate
{
    public TickUpdateInfo updateInfo { get; set; } = new TickUpdateInfo();
    void Start()
    {
        ((ITickUpdate)this).Start(); // Setup your update frequence here
    }

    public void TickUpdate()
    {
        
    }
}

