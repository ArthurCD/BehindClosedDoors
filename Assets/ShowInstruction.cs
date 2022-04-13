using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInstruction : MonoBehaviour
{

    public GameObject UI_instruction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl) )
        {
            UI_instruction.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI_instruction.SetActive(true);

        }

       
    }

    // deactivate enter ability if player leaves collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           UI_instruction.SetActive(false);
        }

        
    }
}
