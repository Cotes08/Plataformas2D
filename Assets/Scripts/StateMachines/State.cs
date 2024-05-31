using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    //Los 3 estados fundamentales de las state machines
    public abstract void OnEnterState(EnemyController controller);

    public abstract void OnUpdateState();

    public abstract void OnExitState();
}
