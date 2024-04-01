using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]
    public float EnemyHealth;
    public float MaxEnemyHealth;
    public float PlayerHealth;
    public float MaxPlayerHealth;
    private bool _Collided = false;
    public FPSController Blocking;
    public HealthAndStaminaBarManager HealthBar;

    [Header("Elements")]
    public FireProjectileController Elements;
    public AIPatrol AI;
    private float _NormalSpeed;
    private float _DebuffTimer;
    private bool _FireDebuff;
    private bool _IceDebuff;

    // Start is called before the first frame update
    void Start()
    {
        // Set max health value for both player and enemy
        MaxPlayerHealth = PlayerHealth;
        MaxEnemyHealth = EnemyHealth;

        // Set debuffs to false
        _DebuffTimer = 3f;
        _NormalSpeed = AI.Agent.speed;
        _FireDebuff = false;
        _IceDebuff = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debuffs();
    }

    // Decrease HP on hit
    private void OnCollisionEnter(Collision collision)
    {
        // Enemy damage
        if (collision.gameObject.tag == "Bullet" && this.gameObject.tag == "Enemy" && !_Collided)
        {
            _Collided = true;
            EnemyHealth -= 5f;
            // Elemental Debuffs
            ElementDebuff();
            HealthBar.UpdateHealthbar(MaxEnemyHealth, EnemyHealth);
            // Destroy this object if health is 0
            if (EnemyHealth <= 0f)
            {
                Destroy(this.gameObject);
            }
            else
            {
                // Make this to false so the enemy can hit me again
                _Collided = false;
            }

            Destroy(collision.gameObject);
        }
        // Player damage
        else if (Blocking.IsBlocking == true)
        {
            // Same enemy bullet collision, just less damage
            if (collision.gameObject.tag == "EnemyBullet" && this.gameObject.tag == "Player" && !_Collided)
            {
                _Collided = true;
                PlayerHealth -= 2f;
                HealthBar.UpdateHealthbar(MaxPlayerHealth, PlayerHealth);
                // Destroy this object if health is 0
                if (PlayerHealth <= 0f)
                {
                    Debug.Log("You Are Dead");
                }
                else
                {
                    // Make this to false so I can hit the enemy again
                    _Collided = false;
                }

                Destroy(collision.gameObject);
            }
        }
        else if (Blocking.IsBlocking == false)
        {
            if (collision.gameObject.tag == "EnemyBullet" && this.gameObject.tag == "Player" && !_Collided)
            {
                _Collided = true;
                PlayerHealth -= 5f;
                HealthBar.UpdateHealthbar(MaxPlayerHealth, PlayerHealth);
                // Destroy this object if health is 0
                if (PlayerHealth <= 0f)
                {
                    Debug.Log("You Are Dead");
                }
                else
                {
                    // Make this to false so I can hit the enemy again
                    _Collided = false;
                }

                Destroy(collision.gameObject);
            }
        }
    }

    // Function to set the elemental debuffs
    private void ElementDebuff()
    {
        // Check what element is equipped
        if (Elements.Fire == true)
        {
            // Fire Deals DoT
            _FireDebuff = true;
            _IceDebuff = false;
        }
        else if (Elements.Ice == true)
        {
            // Ice Deals Debuff
            _FireDebuff = false;
            _IceDebuff = true;
        }
    }

    // Function to set debuff properties
    private void Debuffs()
    {
        if (_FireDebuff && !_IceDebuff)
        {
            // DoT Damage
            if (_DebuffTimer > 0)
            {
                _DebuffTimer -= Time.deltaTime;
                EnemyHealth -= Time.deltaTime;
            }
            HealthBar.UpdateHealthbar(MaxEnemyHealth, EnemyHealth);
        }
        else if (!_FireDebuff && _IceDebuff)
        {
            // Slow down Enemies
            if (_DebuffTimer > 0)
            {
                _DebuffTimer -= Time.deltaTime;
                AI.Agent.speed = 1f;
            }
        }

        if (_DebuffTimer <= 0)
        {
            // Remove existing debuffs and reset timer
            _DebuffTimer = 3f;
            AI.Agent.speed = _NormalSpeed;
            _FireDebuff = false;
            _IceDebuff = false;
        }
    }
}
