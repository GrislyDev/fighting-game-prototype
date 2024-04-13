using UnityEngine;

public class PlayerAttackState : State
{
	private const string ATTACK1 = "Attack1";
	private const string ATTACK2 = "Attack2";
	private Player _player;

	public PlayerAttackState(Player player, StateMachine StateMachine) : base(player, StateMachine)
	{
		_player = player;
	}

	public override void AnimationTriggerEvent(Character.AnimataionTriggerType triggerType)
	{
		if(triggerType == Character.AnimataionTriggerType.CharacterAttacked)
			_player.Attack();

		if (triggerType == Character.AnimataionTriggerType.EndOfAnim)
		{
			_player.StateMachine.ChangeState(_player.PlayerIdleState, _player.Animator);
		}
	}

	public override void EnterState(Animator animator)
	{
		var randomAnim = Random.Range(0, 2);

		if (randomAnim == 1)
			animator.SetTrigger(ATTACK1);
		else
			animator.SetTrigger(ATTACK2);

		SoundManager.Instance.PlaySound(_player.Settings.AttackClip);
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
	}

	public override void PhysicsUpdate()
	{
	}
}