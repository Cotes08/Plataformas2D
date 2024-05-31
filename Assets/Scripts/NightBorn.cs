using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBorn : Enemy
{
    [Header("Combat System NightBorn")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask whatIsDamageble;

    private void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    //Se lanza desde la animacion
    private void LaunchAttackOnAnim()
    {
        //Cogemos todos los enemigos impacatdos por nuestro punto de ataque y les quitamos vida
        Collider2D[] collidersHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsDamageble);
        foreach (var item in collidersHit)
        {
            LifesSystem lifesSystem = item.gameObject.GetComponentInChildren<LifesSystem>();
            lifesSystem.HandleDamage(AttackDamage);
        }
    }


    //Audios del enemigo
    private void PlayRun()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
    protected override void PlayAttack()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    private void PlayHit()
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
