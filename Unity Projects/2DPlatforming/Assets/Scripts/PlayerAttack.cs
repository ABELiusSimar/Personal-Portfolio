using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attacking Variables")]
    [SerializeField] private float _AttackCooldown;
    [SerializeField] private Transform _FirePoint;
    [SerializeField] private GameObject[] _Fireballs;
    private Animator _Anim;
    private PlayerMovement _PlayerMovement;
    private float _CooldownTimer = Mathf.Infinity; // To prevent player from unable to attack at start of game

    private void Awake()
    {
        _Anim = GetComponent<Animator>();
        _PlayerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _CooldownTimer > _AttackCooldown && _PlayerMovement.canAttack())
        {
            Attack();
        }
        _CooldownTimer += Time.deltaTime;
    }

    #region Functions
    // Attacking functions
    private void Attack()
    {
        _Anim.SetTrigger("Attack");
        _CooldownTimer = 0;

        // Pool Fireball
        _Fireballs[FindFireball()].transform.position = _FirePoint.position;
        _Fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < _Fireballs.Length; i++)
        {
            if (!_Fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    #endregion
}
