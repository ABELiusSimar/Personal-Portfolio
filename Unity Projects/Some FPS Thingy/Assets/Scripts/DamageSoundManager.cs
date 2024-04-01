using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSoundManager : MonoBehaviour
{
    private bool _Collided = false;
    public AudioSource Damage;
    public AudioClip[] TakingHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" && this.gameObject.tag == "Player" && !_Collided)
        {
            // Bullet collided
            _Collided = true;
            // When take damage, play random audio clip
            var random = Random.Range(0, 2);
            Damage.PlayOneShot(TakingHit[random]);
        }

        _Collided = false;
    }
}
