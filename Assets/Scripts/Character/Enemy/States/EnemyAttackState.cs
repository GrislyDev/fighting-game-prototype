using UnityEngine;

public class EnemyAttackState : State
{
	private Enemy _enemy;
	public EnemyAttackState(Enemy enemy, StateMachine StateMachine) : base(enemy, StateMachine)
	{
		_enemy = enemy;
	}

	public override void AnimationTriggerEvent(Character.AnimataionTriggerType triggerType)
	{
		if (triggerType == Character.AnimataionTriggerType.CharacterAttacked)
		{
			_enemy.Attack();
		}

		if (triggerType == Character.AnimataionTriggerType.EndOfAnim)
		{
			_enemy.StateMachine.ChangeState(_enemy.EnemyIdleState, _enemy.Animator);
		}
	}

	public override void EnterState(Animator animator)
	{
		animator.SetTrigger("Attack");
		SoundManager.Instance.PlaySound(_enemy.Settings.AttackClip);
	}

	public override void ExitState(Animator animator)
	{
	}

	public override void FrameUpdate()
	{
		if (!GameManager.Instance.IsGameOn)
		{
			_enemy.StateMachine.ChangeState(_enemy.EnemyIdleState, _enemy.Animator);
		}
		if (_enemy.IsHit == true)
		{
			_enemy.StateMachine.ChangeState(_enemy.EnemyHitState, _enemy.Animator);
		}
	}

	public override void PhysicsUpdate()
	{
	}
}