using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Move : BaseState
{
    private TankStateMachine _TankStateMachine;
    
    // all these vars need to placed properly from tankmanager
    private float m_MovementInputValue;    
    private float m_TurnInputValue; 
    private float m_Speed = 12f;            
    private float m_TurnSpeed = 180f;  
    private Rigidbody m_Rigidbody;
    private TankManager _TankManager;

    public Move(TankStateMachine stateMachine, TankManager tankManager) : base("Move", stateMachine)
    {
        _TankStateMachine = stateMachine;
        _TankManager = tankManager;
        m_Rigidbody = tankManager.m_Rigidbody;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("in move state enter");
        // do some basic initialisations like reset all
        
    }
    
    public void MoveTank()
    {
        // Adjust the position of the tank based on the player's input.
        // Use Lerp to smooth the transition
        Vector3 diffVec = _TankManager.transform.forward * _TankManager.m_MovementInputValue * m_Speed * Time.deltaTime;
        _TankManager.m_Rigidbody.MovePosition(_TankManager.m_Rigidbody.position + diffVec);
    }


    public void TurnTank()
    {
        // Adjust the rotation of the tank based on the player's input.
        float diffVec = _TankManager.m_TurnInputValue * m_TurnSpeed  * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, diffVec, 0);
        // make transition smooth
        _TankManager.m_Rigidbody.MoveRotation(_TankManager.m_Rigidbody.rotation * rotation);
    }
    
    public override void UpdateLogic()
    {
        //on click of a button goto move state
        base.UpdateLogic();
//        stateMachine.ChangeState(_TankStateMachine.moveState);
    }
}