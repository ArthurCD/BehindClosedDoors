using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MentalState : MonoBehaviour
{
    public float m_RTPC_PLYR_SelfConscious;
    public float m_RTPC_PLYR_Fear;


    public AK.Wwise.RTPC RTPC_PLYR_SelfConscious;
    public AK.Wwise.RTPC RTPC_PLYR_Fear;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_RTPC_PLYR_SelfConscious = m_RTPC_PLYR_SelfConscious + 0.01f;
        RTPC_PLYR_SelfConscious.SetGlobalValue(m_RTPC_PLYR_SelfConscious);
    }
}
