using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatChaseState : State
{

    private BatController main;
    private Animator anim;

    [SerializeField] private float chaseVelocity;

    public override void OnEnterState(EnemyController controller)
    {
        main = controller as BatController;
        anim = GetComponentInChildren<Animator>();
    }

    public override void OnUpdateState()
    {
        if (anim != null)
        {
            anim.SetBool("Flying", false);
        }
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        LookAtTarget();
        //Obtenemos la posicion del bat target (player) y nos movemos hacia el
        transform.position = Vector3.MoveTowards(transform.position, main.BatTarget.position, chaseVelocity * Time.deltaTime);

        if (Vector3.Distance(transform.position, main.BatTarget.position) < main.AttackRange)
        {
            main.ChangeState(main.AttackState);
        }
    }


    public override void OnExitState(){}

    //Si sale del area de deteccion volvemos al estado patrol
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (anim != null) anim.SetBool("Flying", true);

        if (collision.CompareTag("PlayerDetection"))
        {
            main.ChangeState(main.PatrolState);
        }
    }

    private void LookAtTarget()
    {
        if (main.BatTarget.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
