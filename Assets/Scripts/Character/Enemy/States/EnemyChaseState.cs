using UnityEngine;

public class EnemyChaseState : State
{
	private Enemy _enemy;
	private Transform _playerTransform;
	private float _movementSpeed;
	private float _colliderYOffset = -1.65f;

	public EnemyChaseState(Enemy enemy, StateMachine StateMachine) : base(enemy, StateMachine)
	{
		_enemy = enemy;
		_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public override void AnimationTriggerEvent(Character.AnimataionTriggerType triggerType)
	{
		base.AnimationTriggerEvent(triggerType);
	}

	public override void EnterState(Animator animator)
	{
		animator.SetBool("Walk", true);
		_movementSpeed = _enemy.Settings.GetRandomMovementSpeed();
	}

	public override void ExitState(Animator animator)
	{
		animator.SetBool("Walk", false);
		_enemy.RigidBody.velocity = Vector3.zero;
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
		if (_enemy.IsWithinStrikingDistance)
		{
			_enemy.StateMachine.ChangeState(_enemy.EnemyIdleState, _enemy.Animator);
		}
	}

	public override void PhysicsUpdate()
	{
		if (_enemy.transform.position.y < _colliderYOffset && GameManager.Instance.IsGameOn)
		{
			Vector2 moveDirection = (_playerTransform.position - _enemy.transform.position).normalized;
			_enemy.MoveEnemy(moveDirection * _movementSpeed);
		}
	}
}