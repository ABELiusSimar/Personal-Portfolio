using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Fireball Variables")]
    [SerializeField] private float _Speed;
    private float _Direction;
    private bool _Hit;
    private float _Lifetime;
    private BoxCollider2D _BoxCollider;
    private Animator _Anim;

    private void Awake()
    {
        _BoxCollider = GetComponent<BoxCollider2D>();
        _Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_Hit)
        {
            return;
        }
        float movementSpeed = _Speed * Time.deltaTime * _Direction;
        transform.Translate(movementSpeed, 0, 0);

        _Lifetime += Time.deltaTime;
        if (_Lifetime > 5)
        {
            this.gameObject.SetActive(false);
        }
    }

    #region Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _Hit = true;
        _BoxCollider.enabled = false;
        _Anim.SetTrigger("Explode");
    }

    public void SetDirection(float direction)
    {
        _Lifetime = 0;
        _Direction = direction;
        this.gameObject.SetActive(true);
        _Hit = false;
        _BoxCollider.enabled = true;

        // Flip the projectile
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
