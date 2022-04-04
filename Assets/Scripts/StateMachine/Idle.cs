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
        Debug.Log("in idle state");
    }

    public void Setup()
    {
//        _TankManager.m_Movement = _TankManager.gameObject.GetComponent<TankMovement>();
//        _TankManager.m_Shooting = _TankManager.gameObject.GetComponent<TankShooting>();
//        _TankManager.m_CanvasGameObject = _TankManager.gameObject.GetComponentInChildren<Canvas>().gameObject;

//        _TankManager.m_Movement.m_PlayerNumber = _TankManager.m_PlayerNumber;
//        _TankManager.m_Shooting.m_PlayerNumber = _TankManager.m_PlayerNumber;

        _TankManager.m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(_TankManager.m_PlayerColor) 
                                                      + ">PLAYER " + _TankManager.m_PlayerNumber + "</color>";

        MeshRenderer[] renderers = _TankManager.gameObject.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = _TankManager.m_PlayerColor;
        }
//        stateMachine.ChangeState(_TankStateMachine.moveState);
    }
    
    public override void Enter()
    {
        base.Enter();
        // do some basic initialisations like reset all
    }
    
    public override void UpdateLogic()
    {
        //on click of a button goto move state
        base.UpdateLogic();
//        stateMachine.ChangeState(_TankStateMachine.moveState);
    }
}