using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsManager : MonoBehaviour
{
    public AudioSource Footstep;
    public AudioClip Walking, Jumping, Landing, Dashing;
    public CharacterController CharacterController;
    public FPSController Player;
    public GameObject Collider;
    private float _GroundCheckDistance;
    private float _BufferCheckDistance = 0.1f;
    private bool _IsJumping = true;
    private float _OriginalPitch;

    private void Start()
    {
        _OriginalPitch = Footstep.pitch;
    }

    private void Update()
    {
        // Walking
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && CharacterController.isGrounded && !Player.IsDashing)
        {
            if (!Footstep.isPlaying)
            {
                Footstep.clip = Walking;
                Footstep.Play();
            }

            // Running = walking but faster
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Footstep.pitch = 1.8f;
            }
            else
            {
                Footstep.pitch = _OriginalPitch;
            }
        }

        // Dashing
        if (Input.GetKeyDown(KeyCode.LeftControl) || Player.IsDashing)
        {
            Footstep.PlayOneShot(Dashing);
        }

        // Jumping
        _GroundCheckDistance = (Collider.GetComponent<CapsuleCollider>().height / 2) + _BufferCheckDistance;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, _GroundCheckDistance))
        {
            if (!_IsJumping)
            {
                // Audio plays once
                Footstep.PlayOneShot(Landing);
                _IsJumping = true;
            }
        }
        else
        {
            if (_IsJumping)
            {
                // Audio plays once
                Footstep.PlayOneShot(Jumping);
            }
            _IsJumping = false;
        }
    }
}
