using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 100;
    [SerializeField]
    private float curHp = 100;
    [SerializeField]
    private bool isDie = false;
    [SerializeField]
    private bool isImmun = false;
    [SerializeField]
    private float patrolSpeed = 4f;
    [SerializeField]
    private float traceSpeed = 7f;
    [SerializeField]
    private float attackDamage = 10f;
    [SerializeField]
    private float attackDistance = 5f;
    [SerializeField]
    private float traceDistance = 10f;

    public float MaxHp { get { return maxHp; } }
    public float CurHp { get { return curHp; } set { curHp = value;} }
    public bool IsDie { get { return isDie; } set { isDie = value; } }
    public bool IsImmun { get { return isImmun; } set { isImmun = value; } }
    public float PatrolSpeed { get { return patrolSpeed; } }
    public float TraceSpeed { get { return traceSpeed; } }
    public float AttackDamage { get { return attackDamage; } }
    public float AttackDistance { get { return attackDistance; } }
    public float TraceDistance { get { return traceDistance; } }

    public List<Transform> patrolPosList;

    public int _radius = 10;
    public Transform playerTr;
    public int _angle = 90;




}
