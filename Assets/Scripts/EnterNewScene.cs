using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterNewScene : MonoBehaviour
{
    [SerializeField]
    public string sceneName; 
    [SerializeField]
    private bool canEnter=false;
    public int gotoSpawnerID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canEnter )
        {
            SpawnManager.NextSpawnpointID = gotoSpawnerID;
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
