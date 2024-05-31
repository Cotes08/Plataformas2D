using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Mage
{
    Rigidbody2D rb;
    [SerializeField] private float fireBallImpulse;
    private new Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        audioSource = GetComponentInParent<AudioSource>();
        rb.AddForce(transform.right * fireBallImpulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitBox"))
        {
            LifesSystem lifesSystem = collision.gameObject.GetComponentInChildren<LifesSystem>();
            lifesSystem.HandleDamage(AttackDamage);
        }
        animation.SetTrigger("Explode");
    }

    //Se llama al final de la explosion para que se destruya
    private void FireBallExposion() { Destroy(this.gameObject); }

    //Efectos de sonido
    protected override void PlayAttack()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public override void PlayDie()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

}
