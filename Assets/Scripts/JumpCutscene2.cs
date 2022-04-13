using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpCutscene2 : MonoBehaviour
{
    void OnEnable() 
    {
        SceneManager.LoadScene("FinalCutscene2", LoadSceneMode.Single);
    }
}
