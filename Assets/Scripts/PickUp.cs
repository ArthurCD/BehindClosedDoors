using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public Transform pickUpDest;
    bool canPickup = false;
     public GameObject icon_clickUISprite;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
             icon_clickUISprite.SetActive(true);
        
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
             icon_clickUISprite.SetActive(false);
        
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canPickup)
        {
          GetComponent<Collider>().enabled = false;
            this.transform.position = pickUpDest.position;
            this.transform.parent = GameObject.Find("PickupEmpty").transform;
             icon_clickUISprite.SetActive(false);
        }
    }
}
