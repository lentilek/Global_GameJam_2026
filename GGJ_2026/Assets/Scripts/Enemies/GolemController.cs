using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemController : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;

    [SerializeField] private float patrolWaitTime;
    [SerializeField] private float stopAtDistance;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private NavMeshAgent _agent;
    private int _currentPatrolIndex;
    private bool _isWaiting;
    private EnemyState _state = EnemyState.Patrolling;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        animator.SetBool("isWalking", true);
        NextPatrolPoint();
    }
    private void Update()
    {
        switch (_state)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            default:
                break;
        }
    }
    private void Patrol()
    {
        if (_isWaiting) return;
        if (!_agent.pathPending && _agent.remainingDistance <= stopAtDistance)
        {
            StartCoroutine(WaitAtPatrolPoint());
        }
    }
    private IEnumerator WaitAtPatrolPoint()
    {
        _isWaiting = true;
        animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(patrolWaitTime);
        animator.SetBool("isWalking", true);
        NextPatrolPoint();
        _isWaiting = false;
    }
    private void NextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        _agent.SetDestination(patrolPoints[_currentPatrolIndex].position);
        _currentPatrolIndex = (_currentPatrolIndex + 1) % patrolPoints.Length;
        if(_currentPatrolIndex != 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
