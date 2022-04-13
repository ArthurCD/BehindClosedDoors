using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TriggerCutscene1 : MonoBehaviour
{
    
    public Animator SceneFadeAnim;
    // Start is called before the first frame update

     public string sceneName = "Cutscene_end1_Study";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SceneFade()
    {
        SceneFadeAnim.SetTrigger("FadeOut");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           SceneFade();
           StartCoroutine(OnFadeComplete());

        }

    }


    IEnumerator OnFadeComplete()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }
}
