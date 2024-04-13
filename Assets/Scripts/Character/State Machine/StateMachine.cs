using UnityEngine;

public class StateMachine
{
	public State CurrentState { get; set; }
	public void Initialize(State state, Animator animator)
	{
		CurrentState = state;
		CurrentState?.EnterState(animator);
	}
	public void ChangeState(State state, Animator animator)
	{
		CurrentState.ExitState(animator);
		CurrentState = state;
		CurrentState?.EnterState(animator);
	}
}