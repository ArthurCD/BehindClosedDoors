using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    
    public static CustomCursor instance;

    public Texture2D cursorDefault, cursorInteract;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }


    public void SetCursorInteract()
    {
        Cursor.SetCursor(cursorInteract,Vector2.zero,CursorMode.Auto);
    }

    public void SetCursorDefault()
    {
        Cursor.SetCursor(cursorDefault,Vector2.zero,CursorMode.Auto);
    }


    // Start is called before the first frame update
    void Start()
    {
       SetCursorDefault();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
