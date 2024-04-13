using UnityEngine;

public class State
{
	protected Character _character;
	protected StateMachine _stateMachine;


	public State(Character character, StateMachine stateMachine)
	{
		_character = character;
		_stateMachine = stateMachine;
	}
	public virtual void EnterState(Animator animator) { }
	public virtual void ExitState(Animator animator) { }
	public virtual void FrameUpdate() { }
	public virtual void PhysicsUpdate() { }
	public virtual void AnimationTriggerEvent(Enemy.AnimataionTriggerType triggerType) { }
}
