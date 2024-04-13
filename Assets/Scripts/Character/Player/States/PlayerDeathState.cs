using UnityEngine;

public class PlayerDeathState : State
{
	private const string DEATH_BOOL = "Death";
	private Player _player;

	public PlayerDeathState(Player player, StateMachine StateMachine) : base(player, StateMachine)
	{
		_player = player;
	}

	public override void AnimationTriggerEvent(Character.AnimataionTriggerType triggerType)
	{
	}

	public override void EnterState(Animator animator)
	{
		animator.SetBool(DEATH_BOOL, true);
		SoundManager.Instance.PlaySound(_player.Settings.DeathClip);
		GameManager.Instance.FinishGame();
	}

	public override void ExitState(Animator animator)
	{
	}

	public override void FrameUpdate()
	{
	}

	public override void PhysicsUpdate()
	{
	}
}