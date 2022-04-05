using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayUIOnClick : MonoBehaviour
{
    public GameObject itemUIDisplay;
    public GameObject icon_clickUISprite;
    //[SerializeField]
    //bool canInspect = false;

    /*void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInspect = true;
        }
        if (Input.GetMouseButton(0))
        {
            itemUIDisplay.SetActive(true);
        }
    }*/

    // deactivate inspect ability if player leaves collider
   /* void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInspect = false;
        }
    }*/

    //NEED COLLIDER on object
    private void OnMouseUp()
    {
		//display UI screen
        itemUIDisplay.SetActive(true);  
  
    }

    private void OnMouseOver() {
        icon_clickUISprite.SetActive(true);
    }
    void OnMouseExit()
    {
        icon_clickUISprite.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            itemUIDisplay.SetActive(false);
        }
    }
}
