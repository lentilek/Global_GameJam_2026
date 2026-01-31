using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrolling,
    Following,
    Attacking
}
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStatManager esm;

    [SerializeField] private Transform player;
    [SerializeField] private Transform[] patrolPoints;

    [SerializeField] private float patrolWaitTime;
    [SerializeField] private float stopAtDistance;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private float losePlayerTime = 3f;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCD;

    private NavMeshAgent _agent;
    private int _currentPatrolIndex;
    private bool _isWaiting;
    private EnemyState _state = EnemyState.Patrolling;
    private float _timeSinceLostPlayer;
    private bool _isAttacking, _attackCD;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        esm = GetComponent<EnemyStatManager>();
        _attackCD = false;
    }
    private void Start()
    {
        NextPatrolPoint();
    }
    private void Update()
    {
        Debug.Log(_state.ToString());

        var distanceToPlayer = Vector3.Distance(player.position, transform.position);

        switch (_state)
        {
            case EnemyState.Patrolling:
                Patrol();
                if(distanceToPlayer <= detectionRange && CanSeePlayer())
                {
                    _state = EnemyState.Following;
                }
                break;
            case EnemyState.Following:
                FollowPlayer();
                if(distanceToPlayer <= attackRange)
                {
                    _state = EnemyState.Attacking;
                    StartAttack();
                }
                if (!CanSeePlayer() || distanceToPlayer >= detectionRange * 2)
                {
                    _timeSinceLostPlayer += Time.deltaTime;
                    if (_timeSinceLostPlayer >= losePlayerTime)
                    {
                        _state = EnemyState.Patrolling;
                        ClosestPatrolPoint();
                    }
                }
                else
                {
                    _timeSinceLostPlayer = 0f;
                }
                    break;
            case EnemyState.Attacking:
                if (distanceToPlayer <= attackRange && !_attackCD) Attack();
                if(!_isAttacking && distanceToPlayer > attackRange)
                {
                    _state = EnemyState.Following;
                    _agent.isStopped = false;
                }
                break;

        }
    }
    private void StartAttack()
    {
        _agent.isStopped = true;
        _isAttacking = true;
    }
    private void Attack()
    {
        PlayerControler.Instance.ps.currentHP -= esm.attackDMG;
        _agent.isStopped = true;
        var direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        if(direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        StartCoroutine(AttackCD());
    }
    IEnumerator AttackCD()
    {
        _isAttacking = false;
        _attackCD = true;
        yield return new WaitForSeconds(attackCD);
        _attackCD = false;
    }
    private void FollowPlayer()
    {
        _agent.SetDestination(player.position);
    }
    private void Patrol()
    {
        if(_isWaiting) return;
        if(!_agent.pathPending && _agent.remainingDistance <= stopAtDistance)
        {
            StartCoroutine(WaitAtPatrolPoint());
        }
    }
    private IEnumerator WaitAtPatrolPoint()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(patrolWaitTime);
        NextPatrolPoint();
        _isWaiting = false;
    }
    private void NextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        _agent.SetDestination(patrolPoints[_currentPatrolIndex].position);
        _currentPatrolIndex = (_currentPatrolIndex + 1) % patrolPoints.Length;
    }
    private void ClosestPatrolPoint()
    {
        if(patrolPoints.Length == 0) return;
        var closestIndex = 0;
        var closestDistance = float.MaxValue;

        for (var i = 0; i < patrolPoints.Length; i++)
        {
            var distance = Vector3.Distance(transform.position, patrolPoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        _currentPatrolIndex = closestIndex;
        _agent.SetDestination(patrolPoints[_currentPatrolIndex].position);
    }
    private bool CanSeePlayer()
    {
        return IsFacingPlayer() && HasClearPathToPlayer();
    }
    private bool IsFacingPlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        var angle = Vector3.Angle(transform.forward, dirToPlayer);
        return angle <= viewAngle / 2f;
    }
    private bool HasClearPathToPlayer()
    {
        var dirToPlayer = player.position - transform.position;
        if (Physics.Raycast(transform.position, dirToPlayer.normalized, out RaycastHit hit, dirToPlayer.magnitude))
        {
            return hit.transform == player;
        }

        return true;
    }
}
