using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audio_PersistantVariables : MonoBehaviour
{
    public static Audio_PersistantVariables Instance;

    public Color TeamColor;

    public float m_RTPC_PLYR_SelfConscious;
    public float m_RTPC_PLYR_Fear;


    public AK.Wwise.RTPC RTPC_PLYR_SelfConscious;
    public AK.Wwise.RTPC RTPC_PLYR_Fear;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_RTPC_PLYR_SelfConscious = m_RTPC_PLYR_SelfConscious + 0.01f;
        RTPC_PLYR_SelfConscious.SetGlobalValue(m_RTPC_PLYR_SelfConscious);

        //Debug.Log(TeamColor.ToString());
    }


    public void NewColorSelected(Color color)
    {
        Audio_PersistantVariables.Instance.TeamColor = color;
    }
}
