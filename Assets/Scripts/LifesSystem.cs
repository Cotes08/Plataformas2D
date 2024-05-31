using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesSystem : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private bool isStateMachine;
    private AudioSource audioSource;
    private GameObject enemyGameObject;

    [Header("PlayerEspecifics")]
    [SerializeField] private GameObject Canvas;
    [SerializeField] private bool isPlayer;
    private PlayerInterface playerInterface;

    [Header("Invunerable peroid (player)")]
    [SerializeField] private float invulnerablePeriod = 0;
    private SpriteRenderer spriteRendererPlayer;
    private float invulnerableTime = 0;
    private float blinkingTimer = 0f;
    private int startLayer;

    //Creamo sun getter y setter por que nos viene bien poder acceder a la interfaz del juagdor si lo necesitamos
    public PlayerInterface PlayerInterface { get => playerInterface; set => playerInterface = value; }
    //Health que se usa para la interfaz del jugador y el nightborn
    public float Health { get => health; set => health = value; }

    private void Start()
    {
        //Obtenemos el componente audioSource del GameObject
        audioSource = GetComponent<AudioSource>();
        enemyGameObject = gameObject;//Establecemos un gameobject enemigo defalut
        if (isPlayer)
        {
            startLayer = gameObject.layer;
            spriteRendererPlayer = gameObject.GetComponent<SpriteRenderer>();
            PlayerInterface = Canvas.GetComponent<PlayerInterface>();
        }
        else if(isStateMachine)
        {
            //Si es un enemigo con state machine reescribimos el enemygameobject
            Transform gfGameOject = gameObject.transform.parent.parent;
            enemyGameObject = gfGameOject.gameObject;
        }
    }

    private void Update()
    {
        //En este caso solo lo ejecutara el jugador
        if (invulnerableTime>0)
        {
            invulnerableTime -= Time.deltaTime; //Vamos restando la invulnerabilidad con el tiempo transcurrido
            blinkingTimer += Time.deltaTime;

            if (invulnerableTime <= 0)
            {
                gameObject.layer = startLayer;
                if (spriteRendererPlayer != null)
                {
                    spriteRendererPlayer.color = Color.white;
                }
            }

            if (blinkingTimer >= 0.1f && invulnerableTime != invulnerablePeriod)
            {
                if (spriteRendererPlayer != null)
                {
                    //Le cambiamos el color para que se vea visualmente
                    spriteRendererPlayer.color = spriteRendererPlayer.color == Color.white ? Color.red : Color.white;
                }
                blinkingTimer = 0f;
            }
        }
    }

    //Al recibir daño le restamos vida
    public void HandleDamage(float damage) { 
        Health -= damage;
        if (isPlayer)
        {
            //Si el objetivo golpeado tiene interfaz(la del player) actualizamos su barra de vida
            PlayerInterface.UpdateHealth(Health);
            //Si la invulnerabilidad es mayor a 0 le cambiamos la layer
            if (invulnerablePeriod > 0)
            {
                invulnerableTime = invulnerablePeriod;//2 segundos de invulnerabilidad
                gameObject.layer = 9;//Layer invulnerable

            }
        }
        if (Health<=0)
        {
            //Para no provocar problemas con los audios el player se desactiva en lugar de destruirse
            if (isPlayer)
            {
                gameObject.SetActive(false);
                GameManager.Instance.ActivateAudioListener();
                GameManager.Instance.RestartLevel();
            }
            else
            {
                Enemy enemyScript = gameObject.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    // Llamamos a la función de emitir sonido de muerte que tiene cada enemigo
                    enemyScript.PlayDie();
                }
                // Desactivamos todos los componentes excepto AudioSource
                foreach (var component in GetComponents<Component>())
                {
                    //Evitamos que se deactive el audioSource o el Transform(ya que no se puede)
                    if ((component is not AudioSource) && (component is not Transform))
                    {
                        //Nos aseguramos de desactivar solo lo necesario para que no falle
                        if (component is Behaviour behaviour && behaviour.enabled)
                        {
                            behaviour.enabled = false;
                        }
                    }
                }

                // Destruimos el objeto cuando el sonido haya terminado
                if (audioSource != null && audioSource.clip != null)
                {
                    Destroy(enemyGameObject, audioSource.clip.length);
                    GameManager.Instance.EnemyDestroyed();
                }
                else
                {
                    Destroy(enemyGameObject);
                    GameManager.Instance.EnemyDestroyed();
                }
            }
        }
    }
}
