using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// En esta clase se definen los datos compartidos entre estados y los estados.
public class BatController : EnemyController
{

    [SerializeField] private float attackRange;  //Dato compartido por varios estados: Attack y Chase.
    private Transform batTarget;  //Datos compartido por varios estados: Patrol y Chase.

    //Definimos los estados que va a tener el bat
    private BatPatrolState patrolState;
    private BatChaseState chaseState;
    private BatAttackState attackState;

    public float AttackRange { get => attackRange; }
    public Transform BatTarget { get => batTarget; set => batTarget = value; }
    public BatPatrolState PatrolState { get => patrolState; }
    public BatChaseState ChaseState { get => chaseState; }
    public BatAttackState AttackState { get => attackState; }
    



    // Start is called before the first frame update
    void Start()
    {
        InitSates();
        ChangeState(patrolState);
    }

    private void InitSates()
    {
        patrolState = GetComponent<BatPatrolState>();
        chaseState = GetComponent<BatChaseState>();
        attackState = GetComponent<BatAttackState>();
    }

    protected override void Update()
    {
        base.Update();        
    }
}
