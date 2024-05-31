using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Enemy
{
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Transform fireBallSpawn;
    [SerializeField] private float attackCooldown;
    private Animator anim;

    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    IEnumerator attackRoutine()
    {
        while (true)
        {
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    //Se ejecuta al final de la animacion de ataque
    private void throwFireBall()
    {
        Instantiate(fireBall, fireBallSpawn.position, transform.rotation);
    }

    //Mientras el jugador este dentro de su rango de vision siempre lo va a mirar
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection"))
        {
            LookAtTarget(collision.transform.position);
        }
    }

    //Cuando entre en su rango de vision comenzara el ataque
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection"))
        {
            StartCoroutine(attackRoutine());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection"))
        {
            StopAllCoroutines();
        }
    }

    //Efectos de sonido
    protected override void PlayAttack()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public override void PlayDie()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
}
