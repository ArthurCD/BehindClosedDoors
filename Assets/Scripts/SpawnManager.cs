using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField]
    static public int NextSpawnpointID;

   public GameObject player;
   public Vector3 newSpawnLocation;
   public GameObject[] spawners;
   public int id;

   private void Awake()
   {
       spawners = GameObject.FindGameObjectsWithTag("SpawnPoint");
   }
    // Start is called before the first frame update
    void Start()
    {

        foreach(GameObject spawner in spawners)
        {
            id = spawner.GetComponent<SpawnID>().spawnid;
        
            if( id == NextSpawnpointID)
            {
                newSpawnLocation = spawner.transform.position;
                MovePlayerOnSpawn();
                break;
            }
            
        } 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MovePlayerOnSpawn()
    {
        player.transform.position = newSpawnLocation;
    }
}
