using UnityEngine;

public class TankStateMachine : StateMachine
{
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Move moveState;
    [HideInInspector] public Shoot shootState;

    public void Setup(TankManager tankManager)
    {
        idleState = new Idle(this, tankManager);
        moveState = new Move(this);
        shootState = new Shoot(this, tankManager);
    }

    public override BaseState GetInitialState()
    {
        return idleState;
    }
}