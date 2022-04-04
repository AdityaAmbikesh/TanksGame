using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    BaseState currentState;

    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    void LateUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }

    public void ChangeState(BaseState curState, BaseState newState)
    {
        Debug.Log((newState == null).ToString());
        curState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    public virtual BaseState GetInitialState()
    {
        return null;
    }
}