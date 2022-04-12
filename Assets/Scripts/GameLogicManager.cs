using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    [SerializeField]
    public static bool hasKey = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void CollectKey()
    {
        hasKey = true;
         Debug.Log("Key Collected!");

    }


}
