using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayUIOnClick : MonoBehaviour
{
    public GameObject itemUIDisplay, icon_clickUISprite;

    bool multipleImagesExist = false;
    UIFlipImage flipScript;

    public Animator SceneFade;
    
   
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

    private void Start()
    {
        flipScript = itemUIDisplay.GetComponent<UIFlipImage>();
        if(flipScript != null)
        {
            multipleImagesExist = true;
        }
        else
        {
            multipleImagesExist = false;
        }


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            closeUI();
        }

        if(Input.GetKeyDown(KeyCode.F) && itemUIDisplay.activeSelf)
        {
            if(multipleImagesExist)
                flipScript.FlipImage();

            if (gameObject.tag == "GameKey")
            {
                gameObject.SetActive(false);
                GameLogicManager.CollectKey();
                itemUIDisplay.SetActive(false);
                icon_clickUISprite.SetActive(false);
            }  

            if (gameObject.tag == "EndKey")
            {
                SceneFade.SetTrigger("FadeOut");
                StartCoroutine(EndGameQuit());

            }      


        }


    }


     //NEED COLLIDER on object
    private void OnMouseUp()
    {
		//display UI screen
        itemUIDisplay.SetActive(true);  
        
  
    }

    private void OnMouseOver() 
    {

        icon_clickUISprite.SetActive(true);
        
    

    }
    void OnMouseExit()
    {
        icon_clickUISprite.SetActive(false);
       
         
    }

    private void closeUI()
    {
       itemUIDisplay.SetActive(false);
           
    }

    IEnumerator EndGameQuit()
    {
        yield return new WaitForSeconds(3);
        Application.Quit();
    }

}
