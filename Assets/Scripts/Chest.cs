using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private GameObject player;
    private Animator anim; 
    private bool inRange;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Si el jugador esta en rango y presiona la E abrira el cofre
        if (inRange && Input.GetKeyDown(KeyCode.E)) {
            StartCoroutine(ActivatePowerUp());
            anim.SetBool("OpenChest", true);
            AudioManager.Instance.PlaySFX("OpenChest");
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection")) {
            player = collision.gameObject;
            inRange = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection")) inRange = false;
    }

    private IEnumerator ActivatePowerUp()
    {
        anim.SetBool("OpenChest", true);
        AudioManager.Instance.PlaySFX("OpenChest");
        player.GetComponentInParent<LifesSystem>().PlayerInterface.ShowPowerUp();
        player.GetComponentInParent<Player>().DoubleJump = true;
        AudioManager.Instance.PlaySFX("PowerUp");
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
