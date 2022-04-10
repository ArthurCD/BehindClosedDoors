using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFlipImage : MonoBehaviour
{
    public GameObject UI_Image1, UI_Image2, UI_text1, UI_text2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipImage()
    {
        UI_Image1.SetActive(!UI_Image1.activeSelf);
        UI_text1.SetActive(!UI_text1.activeSelf);
        UI_Image2.SetActive(!UI_Image2.activeSelf);
        UI_text2.SetActive(!UI_text2.activeSelf);
    }

}
