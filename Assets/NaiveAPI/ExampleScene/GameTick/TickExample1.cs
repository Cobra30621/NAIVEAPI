using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaiveAPI.GameTickSystem;
public class TickExample1 : MonoBehaviour,ITickUpdate
{
    public TickUpdateInfo updateInfo { get; set; } = new TickUpdateInfo();
    void Start()
    {
        ((ITickUpdate)this).Start(10); // Setup your update frequence here
    }

    public void TickUpdate()
    {
    }
}
