using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    private Rigidbody2D _Body;
    private float _HorizontalInput;
    [SerializeField] private float _Speed, _JumpPower;
    [SerializeField] private LayerMask _GroundLayer, _WallLayer;
    private Animator _Anim;
    private BoxCollider2D _BoxCollider;
    private float _wallJumpCooldown;

    private void Awake()
    {
        _Body = this.GetComponent<Rigidbody2D>(); // Get Rigidboy component
        _Anim = this.GetComponent<Animator>(); // Get Animator component
        _BoxCollider = this.GetComponent<BoxCollider2D>(); // Get Box Collider Component
    }

    private void Update()
    {
        #region Player Movement
        _HorizontalInput = Input.GetAxis("Horizontal");

        // Flips character from right to left
        if (_HorizontalInput > 0.01f) // Moving to the right
        {
            transform.localScale = Vector3.one;
        }
        else if (_HorizontalInput < -0.01f) // Moving left
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Checks whether player has just wall jump, if not, enter the statement
        if (_wallJumpCooldown < 0.2f)
        {
            _Body.velocity = new Vector2(_HorizontalInput * _Speed, _Body.velocity.y); // Horizontal Movement

            // Checks if player is on wall and not grounded
            if (OnWall() && !IsGrounded())
            {
                // Allow player to stuck on wall
                _Body.gravityScale = 0;
                _Body.velocity = Vector2.zero;
            }
            else
            {
                _Body.gravityScale = 2.5f;
            }

            if (Input.GetKey(KeyCode.Space)) // Jumping
            {
                Jump();
            }
        }
        else
        {
            _wallJumpCooldown += Time.deltaTime;
        }
        #endregion

        #region Animation
        _Anim.SetBool("Run", _HorizontalInput != 0); // Set the boolean value based on player horizontal input
        _Anim.SetBool("Grounded", IsGrounded()); // Set the boolean value based on grounded value
        #endregion
    }

    #region Functions
    // Jump and wall jump function
    private void Jump()
    {
        if (IsGrounded())
        {
            _Body.velocity = new Vector2(_Body.velocity.x, _JumpPower);
            _Anim.SetTrigger("Jump");
        }
        else if (OnWall() && !IsGrounded())
        {
            if (_HorizontalInput == 0)
            {
                _Body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 10); // Pushes player away
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                _Body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 10); // Pushes player away and up
            }
            _wallJumpCooldown = 0;
        }
    }

    // Attacking Function
    public bool canAttack()
    {
        return _HorizontalInput == 0 && IsGrounded() && !OnWall();
    }

    // Grounded Function
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_BoxCollider.bounds.center, _BoxCollider.bounds.size, 0, Vector2.down, 0.1f, _GroundLayer); // Use raycast to check whether there is ground below player
        return raycastHit.collider != null;
    }

    // Wall check Function
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_BoxCollider.bounds.center, _BoxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, _WallLayer); // Use raycast to check whether player is in contact with wall
        return raycastHit.collider != null;
    }
    #endregion
}
