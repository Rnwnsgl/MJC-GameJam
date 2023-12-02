using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.WSA;

public class BasicMonsterController : MonsterController
{
    Animator anim;
    EnemyState enemyState;
    NavMeshAgent nav;
    int patrolOrder = 0;
    [SerializeField]
    Transform curDestination;
    bool isAttack = false;
    Transform playerTr;
    public GameObject bulletPrefab;
    public Transform firePos;
    public GameObject flashPrefab;
    public float bulletspeed = 8f;
    Vector3 Point;
    CharacterController cc;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            enemyState.CurHp -= collision.gameObject.GetComponent<Projectile>().Damage;
            if (enemyState.CurHp <= 0 && !enemyState.IsDie)
                ChangeState(MonsterState.Dead);
            ChangeState(MonsterState.Chase);
        }
    }

    private void Awake()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();
        nav = GetComponent<NavMeshAgent>();
        cc = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnStateEnter(MonsterState state)
    {
        switch (state)
        {
            case MonsterState.Idle:
                IdleEnter();
                break;
            case MonsterState.Patrol:
                PatrolEnter();
                break;
            case MonsterState.Chase:
                ChaseEnter();
                break;
            case MonsterState.Dead:
                DeadEnter();
                break;
        }
    }

    public override void OnStateExit(MonsterState state)
    {
        switch (state)
        {
            case MonsterState.Idle:
                IdleExit();
                break;
            case MonsterState.Patrol:
                PatrolExit();
                break;
            case MonsterState.Chase:
                ChaseExit();
                break;
            case MonsterState.Dead:
                break;
        }
    }

    public override void StateFixedUpdate(MonsterState state)
    {
        switch (state)
        {
            case MonsterState.Idle:
                break;
            case MonsterState.Patrol:
                break;
            case MonsterState.Chase:
                break;
            case MonsterState.Attack:
                break;
            case MonsterState.Dead:
                break;
        }
    }

    public override void StateUpdate(MonsterState state)
    {
        switch (state)
        {
            case MonsterState.Idle:
                IdleUpdate();
                break;
            case MonsterState.Patrol:
                PatrolUpdate();
                break;
            case MonsterState.Chase:
                ChaseUpdate();
                break;
            case MonsterState.Dead:
                break;
        }
    }

    void IdleEnter()
    {

    }

    void PatrolEnter()
    {
        if (enemyState.patrolPosList.Count <= 0)
            ChangeState(MonsterState.Idle);

        anim.SetBool("IsWalk", true);
        nav.SetDestination(enemyState.patrolPosList[patrolOrder].position);
        curDestination = enemyState.patrolPosList[patrolOrder];
    }

    void ChaseEnter()
    {
        anim.SetBool("IsRun", true);
        nav.SetDestination(playerTr.position);

    }

    void AttackEnter()
    {
        if (enemyState.IsDie)
            return;
        transform.LookAt(playerTr);
        isAttack = true;
        anim.SetBool("IsShoot", true);
        nav.isStopped = true;

    }

    void DeadEnter()
    {
        anim.SetTrigger("IsDie");
        anim.SetBool("IsRun", false);
        anim.SetBool("IsShoot", false);
        anim.SetBool("IsWalk", false);

        nav.isStopped = true;
        enemyState.IsDie = true;
        Destroy(gameObject, 10f);
        cc.enabled = false;
    }

    void IdleUpdate()
    {
        //순찰경로가 있는 경우 순찰 상태로 감
        if(enemyState.patrolPosList.Count > 0)
        {
            ChangeState(MonsterState.Patrol);
        }

        //TODO 추격 거리에 들어오면 보였다면 추격
        if (enemyState.TraceDistance > Vector3.Distance(transform.position, playerTr.position))
        {
            ChangeState(MonsterState.Chase);
        }
    }

    void PatrolUpdate()
    {
        //TODO 추격 거리에 들어오면 보였다면 추격
        if (enemyState.TraceDistance > Vector3.Distance(transform.position, playerTr.position))
        {
            ChangeState(MonsterState.Chase);
        }

        //도착했다면 다음 위치로 감
        if (nav.remainingDistance <= 0.2f)
        {
            patrolOrder++;
            if (patrolOrder == enemyState.patrolPosList.Count) patrolOrder = 0;

            nav.SetDestination(enemyState.patrolPosList[patrolOrder].position);
            curDestination = enemyState.patrolPosList[patrolOrder];
        }
        
    }

    void ChaseUpdate()
    {
        if(enemyState.IsDie)
        {
            ChangeState(MonsterState.Dead);
            return;
        }
        // 플레이어의 위치를 계속해서 추적
        if (isAttack)
            return;

        nav.SetDestination(playerTr.position);
        if (enemyState.AttackDistance > Vector3.Distance(transform.position, playerTr.position))
        {
            AttackEnter();
        }

        if (enemyState.TraceDistance < Vector3.Distance(transform.position, playerTr.position))
        {
            ChangeState(MonsterState.Patrol);
        }
    }

    void IdleExit()
    {

    }

    void PatrolExit()
    {
        anim.SetBool("IsWalk", false);
    }

    void ChaseExit()
    {
        anim.SetBool("IsRun", false);
    }

    public void AttackExit()
    {
        isAttack = false;
        anim.SetBool("IsShoot", false);
        nav.isStopped = false;
    }

    public void Shoot()
    {
        GameObject F;
        F = Instantiate(flashPrefab, firePos.position, flashPrefab.transform.rotation);
        F.transform.SetParent(firePos);
        Destroy(F, 1f);

        RaycastHit hit;
        if (Physics.Raycast(firePos.position, firePos.forward, out hit))
        {
            Point = new Vector3(hit.point.x + Random.Range(-0.2f, 0.2f),
                hit.point.y + Random.Range(-0.2f, 0.2f), hit.point.z); ;
        }

        GameObject P;
        var Rel = (Point - firePos.position).normalized;
        P = Instantiate(bulletPrefab, firePos.position, bulletPrefab.transform.rotation);

        P.GetComponent<Rigidbody>().velocity = Rel * bulletspeed;

        AttackExit();
    }

}
