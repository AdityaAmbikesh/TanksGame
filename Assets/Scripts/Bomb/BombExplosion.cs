using System.Collections;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 3000f;            
    public float m_MaxLifeTime = 10f;                  
    public float m_ExplosionRadius = 5f;
    public float bombPlantDuration = 2f;

    private void OnEnable()
    {
        StartCoroutine(WaitForBombLifeCycle(m_MaxLifeTime));
        // collider is enabled after a delay so that planting tank can flee away
        StartCoroutine(EnableColliders(bombPlantDuration));
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
            Collider[] colliders = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_TankMask);

            // Go through all the colliders...
            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

                // If they don't have a rigidbody, go on to the next collider.
                if (!targetRigidbody)
                    continue;
                Explode(targetRigidbody);
            }
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        // Calculate damage as this proportion of the maximum possible damage.
        float damage = relativeDistance * m_MaxDamage;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max (0f, damage);

        return damage;
    }

    private void Explode(Rigidbody targetRigidbody)
    {

        if (targetRigidbody != null)
        {
            // Add an explosion force.
            targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

            // Find the TankHealth script associated with the rigidbody.
            TankManager tankManager = targetRigidbody.GetComponent<TankManager> ();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (tankManager != null)
            {
                float damage = CalculateDamage (targetRigidbody.position);

                // Deal this damage to the tank.
                tankManager.TakeDamage (damage);
            }
        }

        // Play the particle system.
        m_ExplosionParticles.Play();

        ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
        StartCoroutine(SetInactiveAfterLifetime(mainModule.duration));

        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private IEnumerator WaitForBombLifeCycle(float lifecycle)
    {
        yield return new WaitForSeconds(lifecycle);
        Explode(null);
    }

    private IEnumerator SetInactiveAfterLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }

    private IEnumerator EnableColliders(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }
    
}