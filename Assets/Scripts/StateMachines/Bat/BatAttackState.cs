using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackState : State
{
    private BatController main;
    private Animator anim;

    public override void OnEnterState(EnemyController controller)
    {
        main = controller as BatController;
        anim = GetComponentInChildren<Animator>();
    }

    public override void OnUpdateState()
    {
        LaunchAttack();

        if (Vector3.Distance(transform.position, main.BatTarget.position) > main.AttackRange)
        {
            main.ChangeState(main.ChaseState);
        }
    }

    private void LaunchAttack()
    {
        if (anim != null) anim.SetTrigger("Explode");
    }

    //Esto se lanza al final de la explosion para matarse a si mismo
    public void KillBat()
    {
        GetComponentInChildren<LifesSystem>().HandleDamage(1000);
    }

    public override void OnExitState(){}


}
