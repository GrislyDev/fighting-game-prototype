using System;
using UnityEngine;

public class PlayerIdleState : State
{
	private const string IDLE_ANIM = "Idle";
	private Player _player;

	public PlayerIdleState(Player player, StateMachine StateMachine) : base(player, StateMachine)
	{
		_player = player;
	}

	public override void AnimationTriggerEvent(Character.AnimataionTriggerType triggerType)
	{
	}

	public override void EnterState(Animator animator)
	{
		animator.Play(IDLE_ANIM);
	}

	public override void ExitState(Animator animator)
	{
	}

	public override void FrameUpdate()
	{
		if (_player.IsHit == true)
		{
			_player.StateMachine.ChangeState(_player.PlayerHitState, _player.Animator);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			LeftAttackHandler();
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			RightAttackHandler();
		}
	}

	public override void PhysicsUpdate()
	{
	}
	private void LeftAttackHandler()
	{
		_player.SpriteRenderer.flipX = true;
		_player.StateMachine.ChangeState(_player.PlayerAttackState, _player.Animator);
	}
	private void RightAttackHandler()
	{
		_player.SpriteRenderer.flipX = false;
		_player.StateMachine.ChangeState(_player.PlayerAttackState, _player.Animator);
	}


}