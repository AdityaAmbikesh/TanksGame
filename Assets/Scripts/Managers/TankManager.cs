using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TankManager : MonoBehaviour
{
    public Color m_PlayerColor;            
    [HideInInspector] public Transform m_SpawnPoint;         
    [HideInInspector] public int m_PlayerNumber;             
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public int m_Wins;   
    
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;
    public Slider m_AimSlider;
    public int numberOfPooledObjects = 12;
    public Rigidbody m_Shell;            
    public Rigidbody m_Bomb;            
    public Transform m_FireTransform;    
    public Transform m_BombPlantTransform;  
    
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;

    private TankStateMachine _TankStateMachine = null;
    
    private string m_FireButton;
    private string m_BombPlantButton;
    [HideInInspector] public float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;

    private string m_MovementAxisName;     
    private string m_TurnAxisName;  
    [HideInInspector] public float m_MovementInputValue;    
    [HideInInspector] public float m_TurnInputValue;

    [HideInInspector] public Rigidbody m_Rigidbody;
    [HideInInspector] public Transform transform;
    
    [HideInInspector] public ParticleSystem m_ExplosionParticles;   
    private float m_CurrentHealth;  
    [HideInInspector] public bool m_Dead;

    [HideInInspector]public int roundNumber;

    public void Setup()
    {
        if (_TankStateMachine == null)
        {
            _TankStateMachine = new TankStateMachine();
            _TankStateMachine.Setup(this);
        }

        
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        m_FireButton = "Fire" + m_PlayerNumber;
        m_BombPlantButton = "Plant" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        m_Rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        m_ExplosionParticles.gameObject.SetActive(false);
        
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
        SetTankInitialColour();
        
        _TankStateMachine.ChangeState(_TankStateMachine.moveState);

    }

    private void SetTankInitialColour()
    {
        if(m_PlayerNumber == 1)
            m_PlayerColor = Color.red;
        else if(m_PlayerNumber == 2)
            m_PlayerColor = Color.blue;
    }


    public void DisableControl()
    {
//        m_Movement.enabled = false;
//        m_Shooting.enabled = false;

//        m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
//        m_Movement.enabled = true;
//        m_Shooting.enabled = true;

//        m_CanvasGameObject.SetActive(true);
    }


    public void Reset()
    {
        gameObject.transform.position = m_SpawnPoint.position;
        gameObject.transform.rotation = m_SpawnPoint.rotation;

        gameObject.SetActive(false);
        gameObject.SetActive(true);
        Setup();
//        m_CurrentHealth = m_StartingHealth;
    }
    
    private void Update()
    {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
        // Track the current state of the fire button and make decisions based on the current launch force.
        // The slider should have a default value of the minimum launch force.
        m_AimSlider.value = m_MinLaunchForce;
        if (Input.GetButtonDown(m_BombPlantButton))
        {
            _TankStateMachine.ChangeState(_TankStateMachine.shootState);
            _TankStateMachine.shootState.PlantBomb();
        }
        else
        {
            // If the max force has been exceeded and the shell hasn't yet been launched...
            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
                _TankStateMachine.ChangeState(_TankStateMachine.shootState);
                // ... use the max force and launch the shell.
                m_CurrentLaunchForce = m_MaxLaunchForce;
                _TankStateMachine.shootState.Fire ();
            }
            // Otherwise, if the fire button has just started being pressed...
            else if (Input.GetButtonDown (m_FireButton))
            {
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;

            }
            // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
            else if (Input.GetButton (m_FireButton) && !m_Fired)
            {
                // Increment the launch force and update the slider.
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

                m_AimSlider.value = m_CurrentLaunchForce;
            }
            // Otherwise, if the fire button is released and the shell hasn't been launched yet...
            else if (Input.GetButtonUp (m_FireButton) && !m_Fired)
            {
                _TankStateMachine.ChangeState(_TankStateMachine.shootState);
                // ... launch the shell.
                _TankStateMachine.shootState.Fire ();
            }
        }
    }
    
    private void FixedUpdate()
    {
        // Move and turn the tank.
        _TankStateMachine.moveState.MoveTank();
        _TankStateMachine.moveState.TurnTank();
    }
    
    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }
    
    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        // Reduce current health by the amount of damage done.
        m_CurrentHealth -= amount;

        // Change the UI elements appropriately.
        SetHealthUI ();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            _TankStateMachine.ChangeState(_TankStateMachine.deadState);
            _TankStateMachine.deadState.OnDeath();
        }
    }

}
