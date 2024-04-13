using UnityEngine;

public abstract class Enemy : Character, IEnemyMoveable, ITriggerCheckable
{
	[field: SerializeField] public Rigidbody2D RigidBody { get; set; }
	[field: SerializeField] public Animator Animator { get; set; }
	[field: SerializeField] public SpriteRenderer SpriteRenderer { get; set; }	
	[field: SerializeField] public EnemySettings Settings { get; private set; }
	public bool IsFacingRight { get; set; } = true;


	#region STATE_MACHINE_VARIABLES
	public EnemyIdleState EnemyIdleState { get; private set; }
	public EnemyChaseState EnemyChaseState { get; private set; }
	public EnemyAttackState EnemyAttackState { get; private set; }
	public EnemyHitState EnemyHitState { get; private set; }
	public EnemyDeathState EnemyDeathState { get; private set; }
	public bool IsChased { get; set; } = false;
	public bool IsWithinStrikingDistance { get; set; } = false;
	public bool IsHit { get; set; }
	#endregion
	#region UNITY_ENGINE
	private void Awake()
	{		
		MaxHealth = Settings.MaxHealth;
		InitStateMachine();
	}
	private void OnEnable()
	{

		CurrentHealth = MaxHealth;
		StateMachine.Initialize(EnemyIdleState, Animator);
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

		RaycastHit2D[] colliders = Physics2D.RaycastAll(transform.position, raycastDirection, 3f);
		Debug.DrawRay(transform.position, raycastDirection * 3f, Color.blue, .5f);
		Character player;
		int damage = Settings.GetRandomDamagePower();

		foreach (var hit in colliders)
		{
			if (hit.collider.CompareTag("Player"))
			{
				player = hit.collider.GetComponent<Character>();
				player.Damage(damage);
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
	}
	public void MoveEnemy(Vector2 velocity)
	{
		RigidBody.velocity = new Vector2(velocity.x, RigidBody.velocity.y);
		CheckForLeftOrRightFacing(velocity);
	}
	public void CheckForLeftOrRightFacing(Vector2 velocity)
	{
		if (IsFacingRight && velocity.x < 0f)
		{
			SpriteRenderer.flipX = true;
			IsFacingRight = false;
		}
		else if (!IsFacingRight && velocity.x > 0f)
		{
			SpriteRenderer.flipX = false;
			IsFacingRight = true;
		}
	}
	#endregion
	#region DISTANCE_CHECKS
	public void SetChaseStatus(bool isChased)
	{
		IsChased = isChased;
	}
	public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
	{
		IsWithinStrikingDistance = isWithinStrikingDistance;
	}
	#endregion
	#region PRIVATE_METHODS
	private void InitStateMachine()
	{
		StateMachine = new StateMachine();

		EnemyIdleState = new EnemyIdleState(this, StateMachine);
		EnemyChaseState = new EnemyChaseState(this, StateMachine);
		EnemyHitState = new EnemyHitState(this, StateMachine);
		EnemyAttackState = new EnemyAttackState(this, StateMachine);
		EnemyDeathState = new EnemyDeathState(this, StateMachine);
	}
	#endregion
}
