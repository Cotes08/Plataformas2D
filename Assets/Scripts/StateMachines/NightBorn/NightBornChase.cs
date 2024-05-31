using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBornChase : State
{

    private NightBornController main;
    private Animator anim;
    [SerializeField] private float chaseVelocity;
    [SerializeField] private Transform endPoint1;
    [SerializeField] private Transform endPoint2;

    public override void OnEnterState(EnemyController controller)
    {
        main = controller as NightBornController;
        anim = GetComponentInChildren<Animator>();
    }
    public override void OnUpdateState()
    {
        //Como la capa deteccion y la del curpo van por separado para que la capa deteccion no persiga al jugador hasta el infinito le ponemos limitaciones de posicion
        //Ademas paramos la animacion de correr para que parezca que esta esperando
        if (transform.position.x <= endPoint1.position.x && main.NightBornTarget.position.x < endPoint1.position.x)
        {
            if (anim != null) anim.SetBool("Running", false);
            return;
        }
        else if (transform.position.x >= endPoint2.position.x && main.NightBornTarget.position.x > endPoint2.position.x)
        {
            if (anim != null) anim.SetBool("Running", false);
            return;
        }
        ChaseTarget();
    }
    private void ChaseTarget()
    {
        //Miramos al jugador y corre
        LookAtTarget();
        if (anim != null) anim.SetBool("Running", true);

        //El enemigo solo nos sigue en el eje x
        Vector3 targetPosition = new Vector3(main.NightBornTarget.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseVelocity * Time.deltaTime);

        if (Vector3.Distance(transform.position, main.NightBornTarget.position) < main.AttackRange)
        {
            main.ChangeState(main.AttackState);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (anim != null) anim.SetBool("Running", true);

        if (collision.CompareTag("PlayerDetection"))
        {
            main.ChangeState(main.PatrolState);
        }
    }

    public override void OnExitState(){}
    private void LookAtTarget()
    {
        if (main.NightBornTarget.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
}
