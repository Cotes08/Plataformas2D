using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Definimos el estado actual con el state
    protected State currentState;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdateState();
        }
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            //Salimos del state anterior
            currentState.OnExitState();
        }
        //Actualizamos el state por el nuevo 
        currentState = newState;

        //Entramos en el nuevo state y le pasamos el controlador
        currentState.OnEnterState(this);

    }
}
