using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatPatrolState : State
{
    private BatController main;

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;

    private Vector3 actualDestination;
    private int actualIndex = 0;
    private Animator anim;


    public override void OnEnterState(EnemyController controller)
    {
        main = controller as BatController;//Obtenemos una referencia al controlador
        actualDestination = waypoints[actualIndex].transform.position;//La actualDestination es el primer punto
        anim = GetComponentInChildren<Animator>();
        LookAtTarget();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Patrol());
        }  
    }

    public override void OnUpdateState(){}

    public override void OnExitState()
    {
        //Cortamos el hilo de ejecucion de la corrutina
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el objeto colisionado es Player entra en la condicion
        if (collision.CompareTag("PlayerDetection"))
        {
            //Obtenemos la transform del player y se lo pasamos al controlador para que tenga el bat target
            main.BatTarget = collision.transform;

            if (anim!=null)anim.SetTrigger("Attack");
            //Cambiamos el estado a chase
            main.ChangeState(main.ChaseState);
        }
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            while (transform.position != actualDestination)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[actualIndex].position, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            NewDestination();
        }

    }

    //Recorremos el array de posiciones mirando la hacia la posicion donde se dirige el bat
    private void NewDestination()
    {
        actualIndex++;
        if (actualIndex >= waypoints.Length)
        {
            actualIndex = 0;
        }
        actualDestination = waypoints[actualIndex].transform.position;
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        if (actualDestination.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
