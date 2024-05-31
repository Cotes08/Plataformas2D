using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBornPatrol : State
{
    private NightBornController main;

    [SerializeField] private Transform waypoint;
    [SerializeField] private float speed;

    private Vector3 startDestination;
    private Animator anim;

    public override void OnEnterState(EnemyController controller)
    {
        main = controller as NightBornController;//Obtenemos una referencia al controlador
        if (waypoint != null)
        {
            startDestination = waypoint.position;
        } 
        anim = GetComponentInChildren<Animator>();
        LookAtTarget();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Patrol());
        }
    }

    public override void OnExitState()
    {
        //Cortamos el hilo de ejecucion de la corrutina
        StopAllCoroutines();
    }
    public override void OnUpdateState(){}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el objeto colisionado es el Player entra en la condicion
        if (collision.CompareTag("PlayerDetection"))
        {
            main.NightBornTarget = collision.transform;

            //Cambiamos el estado a chase
            main.ChangeState(main.ChaseState);

        }
    }

    //Esta patrulla es esperar en el punto de inicio, es decir, si el player sale de su zona de deteccion volvera a su punto inicial
    IEnumerator Patrol()
    {
        while (transform.position != startDestination)
        {     
           transform.position = Vector3.MoveTowards(transform.position, waypoint.position, speed * Time.deltaTime);  
            yield return new WaitForEndOfFrame();
        }
        if (anim != null) anim.SetBool("Running", false);
        transform.localScale = Vector3.one;
    }

    private void LookAtTarget()
    {
        if (startDestination.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
}
