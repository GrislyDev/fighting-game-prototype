using UnityEngine;

public class EnemyIdleState : State
{
	private Enemy _enemy;

	public EnemyIdleState(Enemy enemy, StateMachine StateMachine) : base(enemy, StateMachine)
	{
		_enemy = enemy;
	}

	public override void AnimationTriggerEvent(Character.AnimataionTriggerType triggerType)
	{
		base.AnimationTriggerEvent(triggerType);
	}

	public override void EnterState(Animator animator)
	{
		animator.Play("Idle");
	}

	public override void ExitState(Animator animator)
	{
	}

	public override void FrameUpdate()
	{
		if (_enemy.IsHit == true)
		{
			_enemy.StateMachine.ChangeState(_enemy.EnemyHitState, _enemy.Animator);
		}
		if (_enemy.IsChased == true && _enemy.IsWithinStrikingDistance == false
			&& _enemy.transform.position.y < -1.64f && GameManager.Instance.IsGameOn)
		{
			_enemy.StateMachine.ChangeState(_enemy.EnemyChaseState, _enemy.Animator);
		}
		else if (_enemy.IsWithinStrikingDistance == true && GameManager.Instance.IsGameOn)
		{
			_enemy.StateMachine.ChangeState(_enemy.EnemyAttackState, _enemy.Animator);
		}
	}

	public override void PhysicsUpdate()
	{
	}
}