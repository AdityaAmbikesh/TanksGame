using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Move : BaseState
{
    private TankStateMachine _TankStateMachine;
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
    
    public override void UpdateLogic()
    {
        //on click of a button goto move state
        base.UpdateLogic();
//        stateMachine.ChangeState(_TankStateMachine.moveState);
    }
}