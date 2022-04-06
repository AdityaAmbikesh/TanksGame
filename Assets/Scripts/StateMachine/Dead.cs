using UnityEngine;

public class Dead : BaseState
{
    private TankStateMachine _TankStateMachine;
    private TankManager _TankManager;
    
    private ParticleSystem m_ExplosionParticles;   
    private bool m_Dead; 
    
    public Dead(TankStateMachine stateMachine, TankManager tankManager) : base("Idle", stateMachine)
    {
        _TankStateMachine = stateMachine;
        _TankManager = tankManager;
        m_ExplosionParticles = _TankManager.m_ExplosionParticles;
        m_Dead = _TankManager.m_Dead;
    }
    
    public override void Enter()
    {
        // Play the effects for the death of the tank and deactivate it.
        // Set the flag so that this function is only called once.
        m_Dead = true;

        // Move the instantiated explosion prefab to the tank's position and turn it on.
        _TankManager.m_ExplosionParticles.transform.position = _TankManager.transform.position;
        _TankManager.m_ExplosionParticles.gameObject.SetActive (true);

        // Play the particle system of the tank exploding.
        _TankManager.m_ExplosionParticles.Play ();

        // Play the tank explosion sound effect.
//        m_ExplosionAudio.Play();

        // Turn the tank off.
        _TankManager.gameObject.SetActive (false);
        base.Enter();
        // do some basic initialisations like reset all
    }
    
    public void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        // Set the flag so that this function is only called once.
        m_Dead = true;

        // Move the instantiated explosion prefab to the tank's position and turn it on.
        _TankManager.m_ExplosionParticles.transform.position = _TankManager.transform.position;
        _TankManager.m_ExplosionParticles.gameObject.SetActive (true);

        // Play the particle system of the tank exploding.
        _TankManager.m_ExplosionParticles.Play ();

        // Play the tank explosion sound effect.
//        m_ExplosionAudio.Play();

        // Turn the tank off.
        _TankManager.gameObject.SetActive (false);
    }
}