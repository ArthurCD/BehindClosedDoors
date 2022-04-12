using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTunnel : MonoBehaviour
{
    
    [SerializeField]
     public string sceneName = "GF_Tunnel";
    [SerializeField]
     bool canEnter = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && GameLogicManager.hasKey && canEnter )
        {
            SceneManager.LoadScene(sceneName);
        }
    }

     // active enter function if player enters collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canEnter = true;
        }

        CustomCursor.instance.SetCursorInteract();
    }

    // deactivate enter ability if player leaves collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canEnter = false;
        }

        CustomCursor.instance.SetCursorDefault();
    }
}
