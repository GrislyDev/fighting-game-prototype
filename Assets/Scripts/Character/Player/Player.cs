using System;
using UnityEngine;

public class Player : Character
{
	public event Action<int> HealthChanged;

	[field: SerializeField] public Rigidbody2D RigidBody { get; set; }
	[field: SerializeField] public Animator Animator { get; set; }
	[field: SerializeField] public SpriteRenderer SpriteRenderer { get; set; }
	[field: SerializeField] public PlayerSettings Settings { get; private set; }

	public bool IsFacingRight { get; set; } = true;


	#region STATE_MACHINE_VARIABLES
	public PlayerIdleState PlayerIdleState { get; set; }
	public PlayerAttackState PlayerAttackState { get; set; }
	public PlayerHitState PlayerHitState { get; set; }
	public PlayerDeathState	PlayerDeathState { get; set; }	
	public bool IsHit { get; set; }
	#endregion

	#region INIT_METHODS
	public void Initialize()
	{
		StateMachine = new StateMachine();
		PlayerIdleState = new PlayerIdleState(this, StateMachine);
		PlayerAttackState = new PlayerAttackState(this, StateMachine);
		PlayerHitState = new PlayerHitState(this, StateMachine);
		PlayerDeathState = new PlayerDeathState(this, StateMachine);

		MaxHealth = Settings.MaxHealth;
	}

	private void Start()
	{
		CurrentHealth = MaxHealth;
		StateMachine.Initialize(PlayerIdleState, Animator);
	}

	private void Update()
	{
		StateMachine.CurrentState.FrameUpdate();
	}
	private void FixedUpdate()
	{
		StateMachine.CurrentState.PhysicsUpdate();
	}
	#endregion
	#region PUBLIC_METHODS
	public override void Attack()
	{
		var raycastDirection = SpriteRenderer.flipX ? Vector2.left : Vector2.right;

		RaycastHit2D[] colliders = Physics2D.RaycastAll(transform.position, raycastDirection, Settings.AttackRange);
		Debug.DrawRay(transform.position, raycastDirection * Settings.AttackRange, Color.red, 0.5f);
		Character enemy;
		int damage = Settings.GetRandomDamagePower();

		foreach (var hit in colliders)
		{
			if (hit.collider.CompareTag("Enemy"))
			{
				enemy = hit.collider.GetComponent<Character>();
				enemy.Damage(damage);
			}
		}
	}
	public override void Damage(int damageAmount)
	{
		if (damageAmount < 0)
		{
			Debug.LogError("Damage can't be less than zero.");
			return;
		}
		
		CurrentHealth -= damageAmount;
		IsHit = true;
		HealthChanged?.Invoke(CurrentHealth);
	}
	#endregion
}
