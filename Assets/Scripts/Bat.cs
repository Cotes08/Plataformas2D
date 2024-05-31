using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    private GameObject parent;

    private void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        parent = this.gameObject.transform.parent.gameObject; 
    }

    //Al colisionar con el jugador le hara daño
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerHitBox"))
        {
            LifesSystem lifesSystem = collision.gameObject.GetComponentInChildren<LifesSystem>();
            lifesSystem.HandleDamage(AttackDamage);
        }
    }

    //Llamada desde la animcion para que el bat se mate
    private void DieAnimation()
    {
        parent.GetComponent<BatAttackState>().KillBat();
    }

    //Efectos de sonido
    private void PlayPatrol()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    private void PlayDetect()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    protected override void PlayAttack()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }

    public override void PlayDie()
    {
        audioSource.clip = audioClips[3];
        audioSource.Play();
    }
}
