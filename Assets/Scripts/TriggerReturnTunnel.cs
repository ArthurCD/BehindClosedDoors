using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerReturnTunnel : MonoBehaviour
{
    [SerializeField]
    public string returnTunnelScene = "GF_ReturnTunnel"; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(returnTunnelScene);
        }
    }
}

        
