using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{	[HideInInspector] public int MaxHealth { get; set; }

	public StateMachine StateMachine { get; set; }
	public int CurrentHealth { get; set; }

	public abstract void Damage(int damageAmount);
	public abstract void Attack();

	#region ANIMATION_TRIGGERS
	private void AnimationTriggerEvent(AnimataionTriggerType triggerType)
	{
		StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
	}
	public enum AnimataionTriggerType
	{
		CharacterAttacked,
		EndOfAnim,
		MidOfAnim
	}
	#endregion
}
