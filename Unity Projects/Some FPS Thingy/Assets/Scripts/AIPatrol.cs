using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Transform Player;
    public GameObject MainPlayer;
    public LayerMask WhatIsGround, WhatIsPlayer;

    [Header("Patrolling")]
    public Vector3 WalkPoint;
    private bool _WalkPointSet;
    public float WalkPointRange;

    [Header("Attacking")]
    public float TimeBetweenAttacks;
    private bool _AlreadyAttacked;
    public GameObject EnemyProjectile;

    [Header("States")]
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    private void Awake()
    {
        Player = MainPlayer.transform;
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight and attack range
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

        //States Possibilities
        if (!PlayerInSightRange && !PlayerInAttackRange)
        {
            Patrolling();
        }
        else if (PlayerInSightRange && !PlayerInAttackRange)
        {
            Chasing();
        }
        else if (PlayerInSightRange && PlayerInAttackRange)
        {
            Attacking();
        }
    }

    // Functions for patrolling
    private void Patrolling()
    {
        // Search for walk points
        if (!_WalkPointSet)
        {
            SearchWalkPoint();
        }

        // Walk to that point
        if (_WalkPointSet)
        {
            Agent.SetDestination(WalkPoint);
        }

        // Reset Walkpoint when reached destination
        Vector3 distanceToWalkPoint = transform.position - WalkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            _WalkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ= Random.Range(-WalkPointRange, WalkPointRange);
        float randomX= Random.Range(-WalkPointRange, WalkPointRange);

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if point is on the ground
        if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround))
        {
            _WalkPointSet = true;
        }
    }

    // Functions for chasing
    private void Chasing()
    {
        Agent.SetDestination(Player.position);
    }

    // Functions for attacking
    private void Attacking()
    {
        // Make sure enemy does not move
        Agent.SetDestination(transform.position);
        transform.LookAt(Player);

        if (!_AlreadyAttacked)
        {
            // Attack Code here
            Rigidbody rb = Instantiate(EnemyProjectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            _AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        _AlreadyAttacked = false;
    }
}
