using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{

    public static GameLogicManager instance;

    [SerializeField]
    public static bool hasKey = false;
    public GameObject UI_keyCollected;


    // Start is called before the first frame update
    
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

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
        instance.UI_keyCollected.SetActive(true);

    }

   

    


}
