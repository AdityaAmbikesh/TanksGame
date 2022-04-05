using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Move : BaseState
{
    private TankStateMachine _TankStateMachine;
    
    // all these vars need to placed properly from tankmanager
    private float m_MovementInputValue;    
    private float m_TurnInputValue; 
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;  
    private Rigidbody m_Rigidbody;         

    public Move(TankStateMachine stateMachine) : base("Move", stateMachine)
    {
        _TankStateMachine = stateMachine;
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
        Vector3 diffVec = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + diffVec);
    }


    public void TurnTank()
    {
        // Adjust the rotation of the tank based on the player's input.
        float diffVec = m_TurnInputValue * m_TurnSpeed  * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, diffVec, 0);
        // make transition smooth
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * rotation);
    }
    
    public override void UpdateLogic()
    {
        //on click of a button goto move state
        base.UpdateLogic();
//        stateMachine.ChangeState(_TankStateMachine.moveState);
    }
}