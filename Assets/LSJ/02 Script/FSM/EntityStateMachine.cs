using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateMachine : MonoBehaviour
{
    protected IEntityState currentState;
    protected IEntityState previousState;

    protected virtual void Update()
    {
        currentState?.OnUpdate();
    }
    protected virtual void FixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }
    public virtual void ChangeState(IEntityState newState)
    {
        if (currentState == newState) return;

        currentState?.OnExit();
        previousState = currentState;

        currentState = newState;
        currentState?.OnEnter();
    }
}
