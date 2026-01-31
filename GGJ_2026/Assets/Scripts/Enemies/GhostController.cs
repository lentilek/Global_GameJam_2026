using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    [SerializeField] private EnemyStatManager esm;

    private Transform player;
    [SerializeField] private Transform[] patrolPoints;

    [SerializeField] private float patrolWaitTime;
    [SerializeField] private float stopAtDistance;
    [SerializeField] private float detectionRange;
    [SerializeField] private float viewAngle;
    [SerializeField] private float losePlayerTime;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCD;

    private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private int _currentPatrolIndex;
    private bool _isWaiting;
    private EnemyState _state = EnemyState.Patrolling;
    private float _timeSinceLostPlayer;
    private bool _isAttacking, _attackCD;
    [SerializeField] private GameObject dmgZone;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        esm = GetComponent<EnemyStatManager>();
        _attackCD = false;
        dmgZone.SetActive(false);
    }
    private void Start()
    {
        player = PlayerControler.Instance.transform;
        NextPatrolPoint();
    }
    private void Update()
    {
        var distanceToPlayer = Vector3.Distance(player.position, transform.position);

        switch (_state)
        {
            case EnemyState.Patrolling:
                Patrol();
                if (distanceToPlayer <= detectionRange)// && CanSeePlayer())
                {
                    _state = EnemyState.Following;
                }
                break;
            case EnemyState.Following:
                FollowPlayer();
                if (distanceToPlayer <= attackRange)
                {
                    _state = EnemyState.Attacking;
                    StartAttack();
                }
                if (distanceToPlayer >= detectionRange * 2)
                {
                    _timeSinceLostPlayer += Time.deltaTime;
                    if (_timeSinceLostPlayer >= losePlayerTime)
                    {
                        _state = EnemyState.Patrolling;
                        _animator.SetBool("isWalking", true);
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
                if (!_isAttacking && distanceToPlayer > attackRange)
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
        _animator.SetBool("isAttacking", true);
        //PlayerControler.Instance.ps.currentHP -= esm.attackDMG;
        _agent.isStopped = true;
        /*var direction = (player.position - transform.position).normalized;
        direction.y = 90f;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }*/
        StartCoroutine(AttackCD());
    }
    IEnumerator AttackCD()
    {
        _isAttacking = false;
        _attackCD = true;
        yield return new WaitForSeconds(.9f);
        dmgZone.SetActive(true);
        dmgZone.GetComponent<DamageZone>().onCD = false;
        yield return new WaitForSeconds(1f);

        _animator.SetBool("isAttacking", false);
        dmgZone.GetComponent<DamageZone>().StopAll();
        dmgZone.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        _attackCD = false;
    }
    private void FollowPlayer()
    {
        _animator.SetBool("isWalking", true);
        _agent.SetDestination(player.position);
        if (gameObject.transform.position.x - player.position.x >= 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
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
        _animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(patrolWaitTime);
        NextPatrolPoint();
        _isWaiting = false;
    }
    private void NextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        _animator.SetBool("isWalking", true);
        _agent.SetDestination(patrolPoints[_currentPatrolIndex].position);
        _currentPatrolIndex = (_currentPatrolIndex + 1) % patrolPoints.Length;
        if (_currentPatrolIndex != 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0,0,0);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    private void ClosestPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
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
        _animator.SetBool("isWalking", true);
        _agent.SetDestination(patrolPoints[_currentPatrolIndex].position);
        if (_currentPatrolIndex != 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
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
