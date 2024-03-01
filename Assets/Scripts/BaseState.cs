using UnityEngine;

public abstract class BaseState 
{
    public abstract void EnterState(StateController);

    public abstract void UpdateState(StateController);

    public abstract void OnCollisionEnter(StateController);
}