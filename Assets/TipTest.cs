using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaiveAPI.GameTickSystem;
public class TipTest : MonoBehaviour,ITickUpdate
{
    public float speed = 1f;
    public TickUpdateInfo updateInfo { get; set; } = new TickUpdateInfo();
    void Start()
    {
        ((ITickUpdate)this).Start(); // Setup your update frequence here
    }

    public void TickUpdate()
    {
        if ((GameTick.CurrentTick / 100) % 2 == 0)
        {
            
            gameObject.transform.position += Vector3.right * speed;
        }
        else
        {
            gameObject.transform.position += Vector3.left * speed;
        }
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("OAO");
        }
        
    }
}
