using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement System")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJump;
    [SerializeField] private Transform feet;
    [SerializeField] private float jumpRadius;
    [SerializeField] private LayerMask whatIsJumpable;
    private bool doubleJump = false;
    private int jumps = 0;


    [Header("Combat System")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask whatIsDamageble;
    [SerializeField] private float attackDamage;

    private new Rigidbody2D rigidbody;
    private float inputH;
    private Animator anim;

    public bool DoubleJump { get => doubleJump; set => doubleJump = value; }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        Jump();
        AttackAnim();
    }

    private void AttackAnim()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }

    //Se ejecuta desde el evento de animacion
    private void Attack()
    {
        //Cogemos todos los enemigos impacatdos por nuestro punto de ataque y les quitamos vida
        Collider2D[] collidersHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsDamageble);
        foreach (var item in collidersHit)
        {
            LifesSystem lifesSystem = item.gameObject.GetComponentInChildren<LifesSystem>();
            if (item.name == "FireBall(Clone)")
            {
                Destroy(item.gameObject);
            }
            else
            {
                lifesSystem.HandleDamage(attackDamage);
            }    
        }
    }

    //Salto del jugador que contempla si tiene o no doble salto
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inFloor())
            {
                rigidbody.AddForce(Vector2.up * playerJump, ForceMode2D.Impulse);
                anim.SetTrigger("Jump");
                jumps++;
            }
            else if (DoubleJump && jumps<2) {
                rigidbody.AddForce(Vector2.up * playerJump, ForceMode2D.Impulse);
                jumps++;
            }    
        }
    }

    private bool inFloor() 
    {
        bool isInFloor = Physics2D.Raycast(feet.position, Vector3.down, jumpRadius, whatIsJumpable);

        if (isInFloor)jumps = 0;
        
        return isInFloor;
    }

    private void Movement()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        //Usamos velocity para que no haya aceleracion en el movimiento
        rigidbody.velocity = new Vector2(inputH * playerSpeed, rigidbody.velocity.y);
        if (inputH != 0)
        {
            anim.SetBool("Running", true);
            if (inputH > 0) 
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }


    //Sonidos ejecutados en el animator
    private void playStep()
    {
        AudioManager.Instance.PlaySFX("PlayerStep");
    }

    private void playJump()
    {
        AudioManager.Instance.PlaySFX("PlayerJump");
    }

    private void playAttack()
    {
        AudioManager.Instance.PlaySFX("PlayerAttack");
    }
}
