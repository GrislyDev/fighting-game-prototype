using UnityEngine;

public class EnemyDeathState : State
{
	private Enemy _enemy;
	private float _currentTime = 0f;
	private float _destroyDelay = 1.6f;

	public EnemyDeathState(Enemy enemy, StateMachine StateMachine) : base(enemy, StateMachine)
	{
		_enemy = enemy;
	}

	public override void AnimationTriggerEvent(Character.AnimataionTriggerType triggerType)
	{
	}

	public override void EnterState(Animator animator)
	{
		_enemy.RigidBody.velocity = Vector3.zero;
		animator.SetBool("Death", true);
		SoundManager.Instance.PlaySound(_enemy.Settings.DeathClip);
		GameManager.Instance.IncrementScore();
		_currentTime = 0;
	}

	public override void ExitState(Animator animator)
	{
	}

	public override void FrameUpdate()
	{
		if (_currentTime > _destroyDelay)
		{
			_enemy.gameObject.SetActive(false);
		}
	}

	public override void PhysicsUpdate()
	{
		_currentTime += 0.02f;
	}
}