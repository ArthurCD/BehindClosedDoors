using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithCorpses : MonoBehaviour
{
     public GameObject returntunneltrigger;

     public GameObject UI_thought1,UI_thought2;

    // Start is called before the first frame update
    void Start()
    {
       /* returntunneltrigger = GameObject.FindWithTag("ReturnTunnelTrigger");
        if (returntunneltrigger != null)
            Debug.Log("Found trigger object!");
           
        returntunnelloader = returntunneltrigger.transform.GetChild(0).gameObject;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            StartCoroutine(DisplayThought1());
           returntunneltrigger.SetActive(true);
           Debug.Log("Return Trigger Activated!");
           

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            StartCoroutine(DisplayThought2());
           

        }
    }

    IEnumerator DisplayThought1()
    {
        UI_thought1.SetActive(true);
        yield return new WaitForSeconds(5);
        //wait 5 seconds 
        UI_thought1.SetActive(false);
        Destroy(UI_thought1);
        
        
    }

    IEnumerator DisplayThought2()
    {
      
         UI_thought2.SetActive(true);
        yield return new WaitForSeconds(5);
        //wait 5 seconds 
        UI_thought2.SetActive(false);
        Destroy(UI_thought2);  
        
    }

}
