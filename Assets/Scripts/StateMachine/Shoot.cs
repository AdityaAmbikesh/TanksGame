using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : BaseState
{
    private TankStateMachine _TankStateMachine;
    
    [HideInInspector] public List<Rigidbody> pooledShells;
    [HideInInspector] public List<Rigidbody> pooledBombs;
    
    private TankManager _TankManager;
    private Transform fireTransform;
    private Transform bombPlantTransform;
    private int numberOfPooledObjects;
    private float m_CurrentLaunchForce;

    private bool m_Fired = false;
    
    public Shoot(TankStateMachine stateMachine, TankManager tankManager) : base("Shoot", stateMachine)
    {
        _TankStateMachine = stateMachine;
        _TankManager = tankManager;
 
        fireTransform = _TankManager.m_FireTransform;
        bombPlantTransform = _TankManager.m_BombPlantTransform;
        numberOfPooledObjects = _TankManager.numberOfPooledObjects;
        m_CurrentLaunchForce = _TankManager.m_CurrentLaunchForce;
        CreatePooledShells();
        CreatePooledBomb();
    }
    public override void Enter()
    {
        base.Enter();
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
        _TankStateMachine.ChangeState(_TankStateMachine.moveState);

    }

    private void CreatePooledShells()
    {
        pooledShells = new List<Rigidbody>();
        for (int i = 0; i < numberOfPooledObjects; i++)
        {
            Rigidbody shell = GameObject.Instantiate(_TankManager.m_Shell);
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
        _TankStateMachine.ChangeState(_TankStateMachine.moveState);
    }
    
    private void CreatePooledBomb()
    {
        pooledBombs = new List<Rigidbody>();
        for (int i = 0; i < _TankManager.numberOfPooledObjects; i++)
        {
            Rigidbody bomb = GameObject.Instantiate(_TankManager.m_Bomb);
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