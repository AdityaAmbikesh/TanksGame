using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : BaseState
{
    private TankStateMachine _TankStateMachine;
    
//    public int m_PlayerNumber = 1;       
//    public Rigidbody m_Shell;            
//    public Rigidbody m_Bomb;            
//    public Transform m_FireTransform;    
//    public Transform m_BombPlantTransform;    
//    public Slider m_AimSlider;

    // this number will be per tank, can optimise to global pool
    // from GameManager(with * number of tanks spawned)
//    public int numberOfPooledObjects = 12;
//    public AudioSource m_ShootingAudio;  
//    public AudioClip m_ChargingClip;     
//    public AudioClip m_FireClip;         
//    public float m_MinLaunchForce = 15f; 
//    public float m_MaxLaunchForce = 30f; 
//    public float m_MaxChargeTime = 0.75f;

    [HideInInspector] public List<Rigidbody> pooledShells;
    [HideInInspector] public List<Rigidbody> pooledBombs;
    
//    private string m_FireButton;
//    private string m_BombPlantButton;
//    private float m_CurrentLaunchForce;  
//    private float m_ChargeSpeed;         
//    private bool m_Fired;

    private TankManager _TankManager;
    private Transform fireTransform;
    private Transform bombPlantTransform;
    private int numberOfPooledObjects;
    private float m_CurrentLaunchForce;

    private bool m_Fired = false;
    
    public Shoot(TankStateMachine stateMachine, TankManager tankManager) : base("Shoot", stateMachine)
    {
        Debug.Log("shoot initailsed");
        _TankStateMachine = stateMachine;
        _TankManager = tankManager;
        
//        m_CurrentLaunchForce = m_MinLaunchForce;
//        m_AimSlider.value = m_MinLaunchForce;
//        
//        m_FireButton = "Fire" + m_PlayerNumber;
//        m_BombPlantButton = "Plant" + m_PlayerNumber;
//
//        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
 
        fireTransform = _TankManager.m_FireTransform;
        bombPlantTransform = _TankManager.m_BombPlantTransform;
        numberOfPooledObjects = _TankManager.numberOfPooledObjects;
        m_CurrentLaunchForce = _TankManager.m_CurrentLaunchForce;
        CreatePooledShells();
        CreatePooledBomb();
    }
    public override void Enter()
    {
        Debug.Log("in shoot enter");
        base.Enter();
        // do some basic initialisations like reset all
    }
    
    public override void UpdateLogic()
    {
        //on click of a button goto move state
        base.UpdateLogic();
//        stateMachine.ChangeState(_TankStateMachine.moveState);
    }
    
    public void Fire()
    {
        // Instantiate and launch the shell.
        // Set the fired flag so only Fire is only called once.
        m_Fired = true;

        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance = GetPooledShell();
        if (shellInstance != null)
        {
            shellInstance.transform.localPosition = fireTransform.position;
            shellInstance.transform.localRotation = fireTransform.rotation;
            shellInstance.GetComponent<MeshRenderer>().enabled = true;
            shellInstance.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("GameInfo : Failed to get pooled shell Instance");
            return;
        }

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_CurrentLaunchForce * fireTransform.forward; 

        // Reset the launch force.  This is a precaution in case of missing button events.
        m_CurrentLaunchForce = _TankManager.m_MinLaunchForce;
    }

    private void CreatePooledShells()
    {
        pooledShells = new List<Rigidbody>();
        for (int i = 0; i < numberOfPooledObjects; i++)
        {
            Rigidbody shell = Instantiate(_TankManager.m_Shell);
            shell.gameObject.SetActive(false);
            pooledShells.Add(shell);
            
        }
    }

    private Rigidbody GetPooledShell()
    {
        foreach (var shell in pooledShells)
        {
            if (!shell.gameObject.activeSelf)
            {
                return shell;
            }
        }

        return null;
    }

    public void PlantBomb()
    {
        Rigidbody bombInstance = GetPooledBomb();
        if (bombInstance != null)
        {
            bombInstance.transform.localPosition = bombPlantTransform.position;
            bombInstance.GetComponent<MeshRenderer>().enabled = true;
            bombInstance.GetComponent<SphereCollider>().enabled = false;
            bombInstance.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("GameInfo : Failed to get pooled bomb Instance");
            return;
        }
    }
    
    private void CreatePooledBomb()
    {
        pooledBombs = new List<Rigidbody>();
        for (int i = 0; i < _TankManager.numberOfPooledObjects; i++)
        {
            Rigidbody bomb = Instantiate(_TankManager.m_Bomb);
            bomb.gameObject.SetActive(false);
            pooledBombs.Add(bomb);
            
        }
    }

    private Rigidbody GetPooledBomb()
    {
        foreach (var bomb in pooledBombs)
        {
            if (!bomb.gameObject.activeSelf)
            {
                return bomb;
            }
        }

        return null;
    }
    
    
}