using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBornController : EnemyController
{

    [SerializeField] private float attackRange;  //Dato compartido por varios estados: Attack y Chase.
    private Transform nightBornTarget;  //Datos compartido por varios estados: Patrol y Chase.

    private NightBornPatrol patrolState;
    private NightBornChase chaseState;
    private NightBornAttack attackState;

    public float AttackRange { get => attackRange; }
    public Transform NightBornTarget { get => nightBornTarget; set => nightBornTarget = value; }
    public NightBornPatrol PatrolState { get => patrolState; }
    public NightBornChase ChaseState { get => chaseState; }
    public NightBornAttack AttackState { get => attackState; }



    // Start is called before the first frame update
    void Start()
    {
        InitSates();
        ChangeState(patrolState);
    }

    private void InitSates()
    {
        patrolState = GetComponent<NightBornPatrol>();
        chaseState = GetComponent<NightBornChase>();
        attackState = GetComponent<NightBornAttack>();
    }

    protected override void Update()
    {
        base.Update();
    }
}
