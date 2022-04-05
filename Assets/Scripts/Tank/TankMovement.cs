using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [HideInInspector]public int m_PlayerNumber;         
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       
//    public AudioSource m_MovementAudio;    
//    public AudioClip m_EngineIdling;       
//    public AudioClip m_EngineDriving;      
//    public float m_PitchRange = 0.2f;

    
    private string m_MovementAxisName;     
    private string m_TurnAxisName;         
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;
    private TankManager _TankManager;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
//        _TankManager = GetComponent<TankManager>();
//        m_PlayerNumber = _TankManager.m_PlayerNumber;
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

//        m_OriginalPitch = m_MovementAudio.pitch;
    }
    

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        // The Input strings are set in input manager
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
        
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
        Turn();
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
        // Use Lerp to smooth the transition
        Vector3 diffVec = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + diffVec);
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        float diffVec = m_TurnInputValue * m_TurnSpeed  * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, diffVec, 0);
        // make transition smooth
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * rotation);
    }
}