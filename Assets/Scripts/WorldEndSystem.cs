using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creado exclusivamente por si el jugador cae por el mapa para matarlo
public class WorldEndSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitBox"))
        {
            LifesSystem lifesSystem = collision.gameObject.GetComponentInChildren<LifesSystem>();
            lifesSystem.HandleDamage(1000);
        }
    }
}
