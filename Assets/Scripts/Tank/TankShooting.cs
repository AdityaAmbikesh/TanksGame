//using Boo.Lang;
//using UnityEngine;
//using UnityEngine.UI;
//
//public class TankShooting : MonoBehaviour
//{
//    public int m_PlayerNumber = 1;       
//    public Rigidbody m_Shell;            
//    public Rigidbody m_Bomb;            
//    public Transform m_FireTransform;    
//    public Transform m_BombPlantTransform;    
//    public Slider m_AimSlider;
//
//    // this number will be per tank, can optimise to global pool
//    // from GameManager(with * number of tanks spawned)
//    public int numberOfPooledObjects = 12;
////    public AudioSource m_ShootingAudio;  
////    public AudioClip m_ChargingClip;     
////    public AudioClip m_FireClip;         
//    public float m_MinLaunchForce = 15f; 
//    public float m_MaxLaunchForce = 30f; 
//    public float m_MaxChargeTime = 0.75f;
//
//    [HideInInspector] public List<Rigidbody> pooledShells;
//    [HideInInspector] public List<Rigidbody> pooledBombs;
//    
//    private string m_FireButton;
//    private string m_BombPlantButton;
//    private float m_CurrentLaunchForce;  
//    private float m_ChargeSpeed;         
//    private bool m_Fired;                
//
//
////    private void OnEnable()
////    {
////        m_CurrentLaunchForce = m_MinLaunchForce;
////        m_AimSlider.value = m_MinLaunchForce;
////    }
//
//
////    private void Start()
////    {
////        m_FireButton = "Fire" + m_PlayerNumber;
////        m_BombPlantButton = "Plant" + m_PlayerNumber;
////
////        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
//////        CreatePooledShells();
////        CreatePooledBomb();
////    }
//    
//
////    private void Update()
////    {
////        // Track the current state of the fire button and make decisions based on the current launch force.
////        // The slider should have a default value of the minimum launch force.
////        m_AimSlider.value = m_MinLaunchForce;
////        if (Input.GetButtonDown(m_BombPlantButton))
////        {
////            PlantBomb();
////        }
////        else
////        {
////            // If the max force has been exceeded and the shell hasn't yet been launched...
////            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
////            {
////                // ... use the max force and launch the shell.
////                m_CurrentLaunchForce = m_MaxLaunchForce;
////                Fire ();
////            }
////            // Otherwise, if the fire button has just started being pressed...
////            else if (Input.GetButtonDown (m_FireButton))
////            {
////                // ... reset the fired flag and reset the launch force.
////                m_Fired = false;
////                m_CurrentLaunchForce = m_MinLaunchForce;
////
////                // Change the clip to the charging clip and start it playing.
//////            m_ShootingAudio.clip = m_ChargingClip;
//////            m_ShootingAudio.Play ();
////            }
////            // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
////            else if (Input.GetButton (m_FireButton) && !m_Fired)
////            {
////                // Increment the launch force and update the slider.
////                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
////
////                m_AimSlider.value = m_CurrentLaunchForce;
////            }
////            // Otherwise, if the fire button is released and the shell hasn't been launched yet...
////            else if (Input.GetButtonUp (m_FireButton) && !m_Fired)
////            {
////                // ... launch the shell.
////                Fire ();
////            }
////        }
////    }
//
//
////    private void Fire()
////    {
////        // Instantiate and launch the shell.
////        // Set the fired flag so only Fire is only called once.
////        m_Fired = true;
////
////        // Create an instance of the shell and store a reference to it's rigidbody.
////        Rigidbody shellInstance = GetPooledShell();
////        if (shellInstance != null)
////        {
////            shellInstance.transform.localPosition = m_FireTransform.position;
////            shellInstance.transform.localRotation = m_FireTransform.rotation;
////            shellInstance.GetComponent<MeshRenderer>().enabled = true;
////            shellInstance.gameObject.SetActive(true);
////        }
////        else
////        {
////            Debug.Log("GameInfo : Failed to get pooled shell Instance");
////            return;
////        }
////
////        // Set the shell's velocity to the launch force in the fire position's forward direction.
////        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 
////
////        // Reset the launch force.  This is a precaution in case of missing button events.
////        m_CurrentLaunchForce = m_MinLaunchForce;
////    }
//
////    private void CreatePooledShells()
////    {
////        pooledShells = new List<Rigidbody>();
////        for (int i = 0; i < numberOfPooledObjects; i++)
////        {
////            Rigidbody shell = Instantiate(m_Shell);
////            shell.gameObject.SetActive(false);
////            pooledShells.Add(shell);
////            
////        }
////    }
//
////    private Rigidbody GetPooledShell()
////    {
////        foreach (var shell in pooledShells)
////        {
////            if (!shell.gameObject.activeSelf)
////            {
////                return shell;
////            }
////        }
////
////        return null;
////    }
////
////    private void PlantBomb()
////    {
////        Rigidbody bombInstance = GetPooledBomb();
////        if (bombInstance != null)
////        {
////            bombInstance.transform.localPosition = m_BombPlantTransform.position;
////            bombInstance.GetComponent<MeshRenderer>().enabled = true;
////            bombInstance.GetComponent<SphereCollider>().enabled = false;
////            bombInstance.gameObject.SetActive(true);
////        }
////        else
////        {
////            Debug.Log("GameInfo : Failed to get pooled bomb Instance");
////            return;
////        }
////    }
//    
////    private void CreatePooledBomb()
////    {
////        pooledBombs = new List<Rigidbody>();
////        for (int i = 0; i < numberOfPooledObjects; i++)
////        {
////            Rigidbody bomb = Instantiate(m_Bomb);
////            bomb.gameObject.SetActive(false);
////            pooledBombs.Add(bomb);
////            
////        }
////    }
////
////    private Rigidbody GetPooledBomb()
////    {
////        foreach (var bomb in pooledBombs)
////        {
////            if (!bomb.gameObject.activeSelf)
////            {
////                return bomb;
////            }
////        }
////
////        return null;
////    }
//    
//    
//    
//    
//}