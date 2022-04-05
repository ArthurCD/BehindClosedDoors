using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEvent : MonoBehaviour
{
    public AK.Wwise.Event FS_Run;
    // Start is called before the first frame update
    void Start()
    {
        FS_Run.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
