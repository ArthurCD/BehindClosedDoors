using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayThought : MonoBehaviour
{
    public GameObject thoughtUI;
    

    //NEED COLLIDER on object
    private void OnMouseUpAsButton()
    {

        StartCoroutine(HasAThought());
	
           
            
    }

    void Update()
    {
    

        
    }

    IEnumerator HasAThought()
    {
       	//display UI screen
           thoughtUI.SetActive(true);
            yield return new WaitForSeconds(5);
             thoughtUI.SetActive(false);
    }
    
}

