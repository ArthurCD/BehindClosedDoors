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


    [SerializeField] private bool m_IsRunning;
    private bool m_IsPostedBreathRun;
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




    public AK.Wwise.Switch Switch_PLYR_Movement_Sneak;
    public AK.Wwise.Switch Switch_PLYR_Movement_Walk;
    public AK.Wwise.Switch Switch_PLYR_Movement_Run;

    public AK.Wwise.Switch Switch_PLYR_Material_Carpet;
    public AK.Wwise.Switch Switch_PLYR_Material_Stone;
    public AK.Wwise.Switch Switch_PLYR_Material_Stone_Stairs;
    public AK.Wwise.Switch Switch_PLYR_Material_Wood;
    public AK.Wwise.Switch Switch_PLYR_Material_Wood_Stairs;



    public AK.Wwise.RTPC RTPC_PLYR_Exhaust;




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
    }


    // Track Player movement
    private void Update()
    {
        CheckTerrain();

        //Debug.Log(rb.velocity.magnitude);

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
                if (m_IsPostedBreathRun==false)
                {
                    m_IsPostedBreathRun = true;
                    PLYR_Breath_Run.Post(gameObject);
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

        hit = Physics.RaycastAll(transform.position, Vector3.down, 0.05f);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Wood"))
            {
                Switch_PLYR_Material_Wood.SetValue(gameObject);

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
            }
        }
    }



    //Post Footsteps Audio Event
    private void PlayFootStepAudio()
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


    private void OnDestroy()
    {
        StopAll.Post(gameObject);
    }

}

    









        //private void ProgressStepCycle(float speed)
        //{
        //    if (!(m_StepCycle > m_NextStep))
        //    {
        //        return;
        //    }

        //    m_NextStep = m_StepCycle + m_StepInterval;

        //    PlayFootStepAudio();
        //}




        //private void UpdateCameraPosition(float speed)
        //{
        //    Vector3 newCameraPosition;
        //    if (!m_UseHeadBob)
        //    {
        //        return;
        //    }
        //    if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
        //    {
        //        m_Camera.transform.localPosition =
        //            m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
        //                              (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
        //        newCameraPosition = m_Camera.transform.localPosition;
        //        newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
        //    }
        //    else
        //    {
        //        newCameraPosition = m_Camera.transform.localPosition;
        //        newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
        //    }
        //    m_Camera.transform.localPosition = newCameraPosition;
        //}



        //private void RotateView()
        //{
        //    m_MouseLook.LookRotation(transform, m_Camera.transform);
        //
