using DG.Tweening;
using UnityEngine;

public class EnemyHitState : State
{
	private Enemy _enemy;
	private Tween _tween;
	private Vector2 _jumpPosition;
	private float _jumpPower = 1f;
	private int _jumpCount = 1;
	private float _jumpDuration = 0.5f;
	private float _minJumpDistance = 1f;
	private float _maxJumpDistance = 3f;

	public EnemyHitState(Enemy enemy, StateMachine StateMachine) : base(enemy, StateMachine)
	{
		_enemy = enemy;
	}

	public override void AnimationTriggerEvent(Character.AnimataionTriggerType triggerType)
	{

		if (triggerType == Character.AnimataionTriggerType.EndOfAnim)
		{
			if (_enemy.CurrentHealth <= 0)
			{
				_tween?.Kill();
				_enemy.StateMachine.ChangeState(_enemy.EnemyDeathState, _enemy.Animator);
			}
			else
			{
				_enemy.StateMachine.ChangeState(_enemy.EnemyIdleState, _enemy.Animator);
			}
		}

		if (triggerType == Character.AnimataionTriggerType.MidOfAnim)
			PushBackEnemy();
	}

	public override void EnterState(Animator animator)
	{
		animator.Play("Hit");
		SoundManager.Instance.PlaySound(_enemy.Settings.HitClip);
	}

	public override void ExitState(Animator animator)
	{
		_enemy.IsHit = false;
	}

	public override void FrameUpdate()
	{
	}

	public override void PhysicsUpdate()
	{
	}

	private void PushBackEnemy()
	{
		float xJumpPosition;
		float yJumpPosition;

		if (_enemy.SpriteRenderer.flipX)
			xJumpPosition = Random.Range(_enemy.transform.position.x + _minJumpDistance, _enemy.transform.position.x + _maxJumpDistance);
		else
			xJumpPosition = Random.Range(_enemy.transform.position.x - _minJumpDistance, _enemy.transform.position.x - _maxJumpDistance);

		yJumpPosition = _enemy.transform.position.y;

		_jumpPosition.x = xJumpPosition;
		_jumpPosition.y = yJumpPosition;

		_tween?.Kill();
		_tween = _enemy.RigidBody.DOJump(_jumpPosition, _jumpPower, _jumpCount, _jumpDuration);
	}
}