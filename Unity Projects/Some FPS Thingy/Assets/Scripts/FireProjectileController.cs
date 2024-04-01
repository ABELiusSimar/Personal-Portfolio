using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileController : MonoBehaviour
{
    public Camera Cam;
    public GameObject ProjectileVoid;
    public GameObject ProjectileFire;
    public GameObject ProjectileIce;
    public Transform LHFirePoint, RHFirePoint;
    public float ProjetileSpeed = 30f;
    public float FireRate = 4;
    public FPSController Blocking;

    private Vector3 _Destination;
    private bool _LeftHand;
    private float _TimeToFire;
    private float _ProjectileDespawnTime;
    private GameObject[] _ProjectilesToDespawn;
    private GameObject[] _EnemyProjectilesToDespawn;

    [Header("Elements")]
    private bool _Void;
    private bool _Fire;
    private bool _Ice;

    // Properties for elements
    public bool Void
    {
        get { return _Void; }
    }

    public bool Fire
    {
        get { return _Fire; }
    }

    public bool Ice
    {
        get { return _Ice; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that void is the starting element
        _Void = true;
        _Fire = false;
        _Ice = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Read element before shooting
        Elements();

        // Read the fire input
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= _TimeToFire)
        {
            _TimeToFire = Time.time + 1 / FireRate;
            // Can't shoot while blocking
            if (Blocking.IsBlocking)
            {
                // Don't shoot
            }
            else
            {
                ShootProjectile();

                // Find all projectiles, player's and enemy's
                _ProjectilesToDespawn = GameObject.FindGameObjectsWithTag("Bullet");
                _EnemyProjectilesToDespawn = GameObject.FindGameObjectsWithTag("EnemyBullet");
            }
        }

        // Despawn player projectile after some time
        if (_ProjectilesToDespawn != null)
        {
            if (Time.time > _ProjectileDespawnTime)
            {
                foreach (GameObject projectile in _ProjectilesToDespawn)
                {
                    Destroy(projectile);
                }

                // Despawn enemy projectiles as well if available
                if (_EnemyProjectilesToDespawn != null)
                {
                    foreach (GameObject projectile in _EnemyProjectilesToDespawn)
                    {
                        Destroy(projectile);
                    }
                }
            }
        } 
    }

    // Function to shoot the projectile
    private void ShootProjectile()
    {
        // Save time for deletion
        _ProjectileDespawnTime = Time.time + 5.0f;

        // Detect the center of screen
        Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // Determine hit of projectile
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            _Destination = hit.point;
        }
        else
        {
            _Destination = ray.GetPoint(1000);
        }

        // Summon projectiles
        if (_LeftHand)
        {
            _LeftHand = false;
            InstantiateProjectile(LHFirePoint);
        }
        else
        {
            _LeftHand = true;
            InstantiateProjectile(RHFirePoint);
        }
    }

    // Function to call projectiles
    private void InstantiateProjectile(Transform firepoint)
    {
        // Instatiate projecctile on each side
        if (_Void && !_Fire && !_Ice)
        {
            var projetileObj = Instantiate(ProjectileVoid, firepoint.position, Quaternion.identity) as GameObject;
            projetileObj.GetComponent<Rigidbody>().velocity = (_Destination - firepoint.position).normalized * ProjetileSpeed;
        }
        else if (!_Void && _Fire && !_Ice)
        {
            var projetileObj = Instantiate(ProjectileFire, firepoint.position, Quaternion.identity) as GameObject;
            projetileObj.GetComponent<Rigidbody>().velocity = (_Destination - firepoint.position).normalized * ProjetileSpeed;
        }
        else if (!_Void && !_Fire && _Ice)
        {
            var projetileObj = Instantiate(ProjectileIce, firepoint.position, Quaternion.identity) as GameObject;
            projetileObj.GetComponent<Rigidbody>().velocity = (_Destination - firepoint.position).normalized * ProjetileSpeed;
        }
    }

    // Function to choose projectiles
    private void Elements()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 1 for void
            _Void = true;
            _Fire = false;
            _Ice = false;

            Debug.Log($"{_Void}, {_Fire}, {_Ice}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 2 for fire
            _Void = false;
            _Fire = true;
            _Ice = false;

            Debug.Log($"{_Void}, {_Fire}, {_Ice}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // 3 for ice
            _Void = false;
            _Fire = false;
            _Ice = true;

            Debug.Log($"{_Void}, {_Fire}, {_Ice}");
        }
    }
}
