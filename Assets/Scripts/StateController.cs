using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    IState currentState

    // Set of possible states
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public PissState pissState = new PissState();
    public CuriousState curiousState = new CuriousState();

    private void Start()
    {
        ChangeState(patrolState);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEnter(this);
    }
}

public interface IState
{
    public void OnEnter(StateController controller);

    public void UpdateState(StateController controller);

    public void OnHurt(StateController controller);

    public void OnExit(StateController controller);
}
