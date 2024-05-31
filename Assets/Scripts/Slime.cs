using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    private Vector3 actualDestination;
    private int actualIndex = 0;
    private Animator anim;


    void Start()
    {
        actualDestination = waypoints[actualIndex].transform.position;
        anim = GetComponent<Animator>();
        audioSource = GetComponentInParent<AudioSource>();
        StartCoroutine(Patrol());
    }

    //El slime estara patrullando constantemente
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

    //Si el jugador entra en su rango de ataque activara la animacion de ataque y le hara daño
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            anim.SetBool("Attacking", true);

        }
        else if (collision.gameObject.CompareTag("PlayerHitBox"))
        {
            LifesSystem lifesSystem = collision.gameObject.GetComponentInChildren<LifesSystem>();
            lifesSystem.HandleDamage(AttackDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            anim.SetBool("Attacking", false);
        } 
    }


    private void NewDestination()
    {
        actualIndex = 1 - actualIndex;
        actualDestination = waypoints[actualIndex].transform.position;
        LookAtTarget(actualDestination);
    }

    //Efectos de sonido del enemigo
    private void PlayPatrol()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    protected override void PlayAttack()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    public override void PlayDie()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
}
