using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Idle : BaseState
{
    private TankStateMachine _TankStateMachine;
    private TankManager _TankManager;

    public Idle(TankStateMachine stateMachine, TankManager tankManager) : base("Idle", stateMachine)
    {
        _TankStateMachine = stateMachine;
        _TankManager = tankManager;
        Setup();
    }

    public void Setup()
    {
        _TankManager.m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(_TankManager.m_PlayerColor) 
                                                      + ">PLAYER " + _TankManager.m_PlayerNumber + "</color>";

        MeshRenderer[] renderers = _TankManager.gameObject.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = _TankManager.m_PlayerColor;
        }
//        stateMachine.ChangeState(_TankStateMachine.moveState);
    }
    
    public void DisableControl()
    {
        // TODO :: disable tank controls
    }
    
    public override void Enter()
    {
        DisableControl();
        base.Enter();
    }
    
    public override void UpdateLogic()
    {
        base.UpdateLogic();
//        stateMachine.ChangeState(_TankStateMachine.moveState);
    }
}