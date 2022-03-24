using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestDisplayUI : MonoBehaviour
{
    public GameObject itemUIDisplay;
    

    //NEED COLLIDER on object
    private void OnMouseUpAsButton()
    {

        
			// object is clickable only if tray and lid are open
           itemUIDisplay.SetActive(true);
           
            
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            itemUIDisplay.SetActive(false);
        }
    }
}
