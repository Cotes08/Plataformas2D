using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] protected AudioClip[] audioClips;
    protected AudioSource audioSource;

    protected float AttackDamage { get => attackDamage; set => attackDamage = value; }

    protected void LookAtTarget(Vector3 actualDestination)
    {
        if (actualDestination.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    protected abstract void PlayAttack();
    public abstract void PlayDie();


}
