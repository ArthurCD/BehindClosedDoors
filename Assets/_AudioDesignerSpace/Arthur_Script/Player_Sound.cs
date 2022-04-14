using System;
using UnityEngine;
using Random = UnityEngine.Random;




/// Arthur
/// 

public class Player_Sound : MonoBehaviour
{
    /// <summary>
    /// Footsteps Type based on velocity and shift key down
    /// 
    /// Footsteps Material based on Ray Tracing and "Layer" check
    /// 
    /// Breath based on Fatigue/FootstepsType/Tension
    /// 
    /// </summary>
    /// 

    //private enum CURRENT_TERRAIN { Wood, Sood_Stairs, Stone, Stone_stairs, Carpet };

    //[SerializeField]
    //private CURRENT_TERRAIN currentTerrain;

    //[SerializeField]
    //private AK.Wwise.Event footstepsEvent;

    //[SerializeField]
    //private AK.Wwise.Switch[] terrainSwitch;









    private Rigidbody rb;

    float timer = 0.0f;
    float m_RTPC_PLYR_Exhaust = .0f;



    private float FootstepSpeed = 1.0f;
    public float FootstepSpeedSneak = .6f;
    public float FootstepSpeedWalk = .5f;
    public float FootstepSpeedRun = .35f;
    public int FootstepSpeedConditioner = 0; //0= Normal     1= only Walk       2= only Run


    [SerializeField] private bool m_IsRunning;
    private bool m_IsPostedBreathRun=false;
    private bool m_IsPostedBreathStop;
    [SerializeField] private float m_WalkSpeed;
    [SerializeField] private float m_RunSpeed;
    [SerializeField] private float m_StepInterval;


    public AK.Wwise.Event StopAll;


    public AK.Wwise.Event FS_Sneak;
    public AK.Wwise.Event FS_Walk;    // an array of footstep sounds that will be randomly selected from.
    public AK.Wwise.Event FS_Run;     //RUN
    public AK.Wwise.Event FS_Stop;
    public AK.Wwise.Event FS_Rotate;
    // public AK.Wwise.Event footstepSound_Jump;           // the sound played when character leaves the ground.
    // public AK.Wwise.Event footstepSound_Land;           // the sound played when character touches back on ground.


    public AK.Wwise.Event PLYR_Breath_Idle;
    public AK.Wwise.Event PLYR_Breath_Run;
    public AK.Wwise.Event PLYR_Breath_Stop;

    public AK.Wwise.Event PLYR_Breath_Oneoff_Distressing;




    public AK.Wwise.Switch Switch_PLYR_Movement_Sneak;
    public AK.Wwise.Switch Switch_PLYR_Movement_Walk;
    public AK.Wwise.Switch Switch_PLYR_Movement_Run;

    public AK.Wwise.Switch Switch_PLYR_Material_Carpet;
    public AK.Wwise.Switch Switch_PLYR_Material_Stone;
    public AK.Wwise.Switch Switch_PLYR_Material_Stone_Stairs;
    public AK.Wwise.Switch Switch_PLYR_Material_Wood;
    public AK.Wwise.Switch Switch_PLYR_Material_Wood_Stairs;



    public AK.Wwise.RTPC RTPC_PLYR_Exhaust;
    public GameObject Audio_PersistantVariables;
    public Audio_PersistantVariables PersistantScript;




    private float m_StepCycle;
    private float m_NextStep;




    //Get Player Character Rigidbody to track it's movements (velocity) 
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Start Breathing
    private void Start()
    {
        //FS_Run.Post(gameObject);
        PLYR_Breath_Idle.Post(gameObject);

        Audio_PersistantVariables = GameObject.FindGameObjectWithTag("Audio_PersistantVariables");
        PersistantScript = Audio_PersistantVariables.GetComponent<Audio_PersistantVariables>();
    }

 
    // Track Player movement
    private void Update()
    {
        CheckTerrain();

        //Debug.Log(rb.velocity.magnitude);
        //Debug.Log(m_IsPostedBreathRun);

        //ILDE
        if (rb.velocity.magnitude < .1f)
        {
            m_IsRunning = false;
            if (m_IsPostedBreathRun == true)
            {
                PLYR_Breath_Stop.Post(gameObject);
                m_IsPostedBreathRun = false;
            }
        }
        else
        {

            //Normal Walk System
            if (FootstepSpeedConditioner == 0)
            {

                //SNEAK
                if (rb.velocity.magnitude < 2.0f)
                {
                    if (m_IsPostedBreathRun == true)
                    {
                        PLYR_Breath_Stop.Post(gameObject);
                        m_IsPostedBreathRun = false;
                    }
                    m_IsRunning = false;
                    FootstepSpeed = FootstepSpeedSneak;
                }

                //RUN
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (m_IsPostedBreathRun == false)
                    {
                        m_IsPostedBreathRun = true;

                        PLYR_Breath_Run.Post(gameObject);
                        Debug.Log("RUNBREATH");
                    }
                    m_IsRunning = true;
                    FootstepSpeed = FootstepSpeedRun;

                    if (m_RTPC_PLYR_Exhaust < 100)
                    {
                        m_RTPC_PLYR_Exhaust = m_RTPC_PLYR_Exhaust + 0.1f;
                        RTPC_PLYR_Exhaust.SetGlobalValue(m_RTPC_PLYR_Exhaust);
                    }
                }

                //WALK
                else
                {
                    if (m_IsPostedBreathRun == true)
                    {
                        PLYR_Breath_Stop.Post(gameObject);
                        m_IsPostedBreathRun = false;
                    }
                    m_IsRunning = false;
                    FootstepSpeed = FootstepSpeedWalk;

                }
            }


            // Walk in the Tunnel
            else if (FootstepSpeedConditioner == 1) 
            {
                if (m_IsPostedBreathRun == true)
                {
                    PLYR_Breath_Stop.Post(gameObject);
                    m_IsPostedBreathRun = false;
                }
                m_IsRunning = false;
                FootstepSpeed = FootstepSpeedWalk;
            }

            

            // Run in the Return Tunnel 
            else if (FootstepSpeedConditioner == 2)
            {
                if (rb.velocity.magnitude > .1f)
                {
                    m_IsRunning = true;
                    
                    FootstepSpeed = FootstepSpeedRun;
                    if (m_IsPostedBreathRun==false)
                    {
                        m_IsPostedBreathRun = true;
                        PLYR_Breath_Run.Post(gameObject);
                    }
                    if (m_RTPC_PLYR_Exhaust <= 100.0f)
                    {
                        m_RTPC_PLYR_Exhaust = m_RTPC_PLYR_Exhaust + 0.1f;
                        RTPC_PLYR_Exhaust.SetGlobalValue(m_RTPC_PLYR_Exhaust);
                    }
                }
                else
                {
                    if (m_IsPostedBreathRun == true)
                    {
                        PLYR_Breath_Stop.Post(gameObject);
                        m_IsPostedBreathRun = false;
                    }
                    m_IsRunning = false;
                    FootstepSpeed = FootstepSpeedWalk;
                    if (m_RTPC_PLYR_Exhaust >= 0.0f)
                    {
                        m_RTPC_PLYR_Exhaust = m_RTPC_PLYR_Exhaust - 0.1f;
                        RTPC_PLYR_Exhaust.SetGlobalValue(m_RTPC_PLYR_Exhaust);
                    }
                }

            }


          

            //Debug.Log(FootstepSpeedWalk);
            //Debug.Log(FootstepSpeed);
            //Debug.Log(timer);


            //Time beetween footsteps
            if (timer > FootstepSpeed)
            {
                PlayFootStepAudio();
                timer = 0.0f;
            }   
        }


        //Exhaust decrease
        if (!m_IsRunning && m_RTPC_PLYR_Exhaust > 0)
        {
            m_RTPC_PLYR_Exhaust = m_RTPC_PLYR_Exhaust - 0.1f;
            RTPC_PLYR_Exhaust.SetGlobalValue(m_RTPC_PLYR_Exhaust);
        }

        timer += Time.deltaTime;
        //Debug.Log(m_RTPC_PLYR_Exhaust);
     
    }


    // Switch Walking Material
    //Ray slightly under character to check if a registered "Layer" is set to the walked game object
    private void CheckTerrain()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position, Vector3.down, 0.02f);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Wood"))
            {
                Switch_PLYR_Material_Wood.SetValue(gameObject);
                //Debug.Log("Wood");


                //currentTerrain = CURRENT_TERRAIN.WOOD;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Wood_Stairs"))
            {
                Switch_PLYR_Material_Wood_Stairs.SetValue(gameObject);

                //currentTerrain = CURRENT_TERRAIN.WOOD_STAIRS;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Stone"))
            {
                Switch_PLYR_Material_Stone.SetValue(gameObject);
                //Debug.Log("Stone");

                //currentTerrain = CURRENT_TERRAIN.STONE;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Stone_Stairs"))
            {
                Switch_PLYR_Material_Stone_Stairs.SetValue(gameObject);
            

                //currentTerrain = CURRENT_TERRAIN.STONE_STAIRS;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Carpet"))
            {
                Switch_PLYR_Material_Carpet.SetValue(gameObject);

                //currentTerrain = CURRENT_TERRAIN.CARPET;
                //Debug.Log("Carpet");

            }
        }
    }



    //Post Footsteps Audio Event
    private void PlayFootStepAudio()
    {

        //Normal Walk System
        if (FootstepSpeedConditioner == 0)
        {
            if (m_IsRunning)
            {
                Switch_PLYR_Movement_Run.SetValue(gameObject);
                FS_Run.Post(gameObject);
                //Debug.Log("Run");
            }

            else if (rb.velocity.magnitude < 2.0f)
            {
                Switch_PLYR_Movement_Sneak.SetValue(gameObject);
                FS_Sneak.Post(gameObject);
                //Debug.Log("Sneak");

            }

            else
            {
                Switch_PLYR_Movement_Walk.SetValue(gameObject);
                FS_Walk.Post(gameObject);
                //Debug.Log("Walk");
            }
        }

        // Walk in the Tunnel
        else if (FootstepSpeedConditioner == 1)
        {
            Switch_PLYR_Movement_Walk.SetValue(gameObject);
            FS_Walk.Post(gameObject);
        }


        // Run in the Return Tunnel 
        else if (FootstepSpeedConditioner == 2)
        {
            Switch_PLYR_Movement_Run.SetValue(gameObject);
            FS_Run.Post(gameObject);
        }
    }



    public void Collectible_Distressing()
    {
        Debug.Log("Collect");
        PLYR_Breath_Oneoff_Distressing.Post(gameObject);
        PersistantScript.m_DistressingItem = true;
    }








    private void OnDestroy()
    {
        StopAll.Post(gameObject);
    }

}

    
