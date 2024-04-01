using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [Header("Classes")]
    public FPSController Player;
    public HealthManager PlayerHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "EnemyBullet")
        {
            // Don't trigger
        }
        else
        {
            if (this.gameObject.tag == "Speed")
            {
                Player.WalkingSpeed += 100f;
                Player.RunningSpeed += 100f;
                Debug.Log("Speed Increased");

            }
            else if (this.gameObject.tag == "Health")
            {
                PlayerHealth.PlayerHealth += 20;
                Debug.Log("Health Increased");
            }
        }
    }
}
