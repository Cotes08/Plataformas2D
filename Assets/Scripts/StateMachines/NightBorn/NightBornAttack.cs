using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBornAttack : State
{
    private NightBornController main;
    private Animator anim;
    private LifesSystem lifesSystem;
    private float nightBornLifes;
    private GameObject childObject;

    public override void OnEnterState(EnemyController controller)
    {
        main = controller as NightBornController;
        anim = GetComponentInChildren<Animator>();
        lifesSystem = GetComponentInChildren<LifesSystem>();
        nightBornLifes = lifesSystem.Health;
    }
    public override void OnUpdateState()
    {
        //Vamos obteniendo las vidas para saber cuando debemos ejecutar la corrutina de enemigoDañado
        if (lifesSystem.Health != nightBornLifes)
        {
            if (gameObject.activeInHierarchy && lifesSystem.Health > 0)
            {
                StartCoroutine(GetHit());
            }
        }

        if (anim!=null) anim.SetBool("Running", false);//Detenemos la animacion de correr
        if (anim != null) anim.SetTrigger("Attack");//Lanzamos el ataque
        if (Vector3.Distance(transform.position, main.NightBornTarget.position) > main.AttackRange)
        {
            main.ChangeState(main.ChaseState);
        }
    }

    public override void OnExitState(){}

    private IEnumerator GetHit()
    {
        anim.SetTrigger("Hit");//Ejecutamos la animacion de daño
        childObject = transform.Find("NightBornVisual").gameObject;
        childObject.layer = 9;//Le pasamos a la layer invulnerable
        nightBornLifes = lifesSystem.Health;
        yield return new WaitForSeconds(1);//Lo hacemos invulnerable 1 segundo (un poco mas que la animacion)
        childObject.layer = 6;//Layer Hitbox
    }
}
