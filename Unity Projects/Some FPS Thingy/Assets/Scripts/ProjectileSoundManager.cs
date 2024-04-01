using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSoundManager : MonoBehaviour
{
    public AudioSource Shoot;
    public AudioClip VoidProjectileClip, FireballClip, IceshardClip;
    public FireProjectileController FireRate, Elements;
    private float _TimeToFire;

    // Update is called once per frame
    void Update()
    {
        // Sound based on selecting such element
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 1 for void
            Shoot.PlayOneShot(VoidProjectileClip);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 2 for fire
            Shoot.PlayOneShot(FireballClip);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // 3 for ice
            Shoot.PlayOneShot(IceshardClip);
        }

        // Sound based on the element
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= _TimeToFire)
        {
            _TimeToFire = Time.time + 1 / FireRate.FireRate;
            // Play Audio based on selected Elements
            if (Elements.Void)
            {
                Shoot.PlayOneShot(VoidProjectileClip);
            }
            else if (Elements.Fire)
            {
                Shoot.PlayOneShot(FireballClip);
            }
            else if (Elements.Ice)
            {
                Shoot.PlayOneShot(IceshardClip);
            }
        }
    }
}
