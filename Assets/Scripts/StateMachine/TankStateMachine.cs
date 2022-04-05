using UnityEngine;

public class TankStateMachine : StateMachine
{
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Move moveState;
    [HideInInspector] public Shoot shootState;
    [HideInInspector] public Dead deadState;

    public void Setup(TankManager tankManager)
    {
        idleState = new Idle(this, tankManager);
        moveState = new Move(this, tankManager);
        shootState = new Shoot(this, tankManager);
        deadState = new Dead(this, tankManager);
    }

    public override BaseState GetInitialState()
    {
        return idleState;
    }
}