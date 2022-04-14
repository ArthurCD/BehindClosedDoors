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


    public bool m_DistressingItem = false;

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

        if (m_DistressingItem == true)
        {
            m_RTPC_PLYR_SelfConscious = 100;
            RTPC_PLYR_SelfConscious.SetGlobalValue(m_RTPC_PLYR_SelfConscious);
            m_DistressingItem = false;
        }
        else
        {
            if(m_RTPC_PLYR_SelfConscious > 0.0f)
            {
                m_RTPC_PLYR_SelfConscious = m_RTPC_PLYR_SelfConscious - 0.03f;
                RTPC_PLYR_SelfConscious.SetGlobalValue(m_RTPC_PLYR_SelfConscious);
            }
        }
  

        //Debug.Log(TeamColor.ToString());
    }


    public void NewColorSelected(Color color)
    {
        Audio_PersistantVariables.Instance.TeamColor = color;
    }
}
