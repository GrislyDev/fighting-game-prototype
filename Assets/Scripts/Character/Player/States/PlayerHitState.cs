using UnityEngine;

public class PlayerHitState : State
{
	private const string HIT_ANIM = "Hit";
	private Player _player;

	public PlayerHitState(Player player, StateMachine StateMachine) : base(player, StateMachine)
	{
		_player = player;
	}

	public override void AnimationTriggerEvent(Character.AnimataionTriggerType triggerType)
	{
		if (triggerType == Character.AnimataionTriggerType.EndOfAnim)
		{
			if (_player.CurrentHealth <= 0)
			{
				_player.StateMachine.ChangeState(_player.PlayerDeathState, _player.Animator);
			}
			else
			{
				_player.StateMachine.ChangeState(_player.PlayerIdleState, _player.Animator);
			}
		}
	}

	public override void EnterState(Animator animator)
	{
		animator.Play(HIT_ANIM);
		SoundManager.Instance.PlaySound(_player.Settings.HitClip);
	}

	public override void ExitState(Animator animator)
	{
		_player.IsHit = false;
	}

	public override void FrameUpdate()
	{
	}

	public override void PhysicsUpdate()
	{
	}
}