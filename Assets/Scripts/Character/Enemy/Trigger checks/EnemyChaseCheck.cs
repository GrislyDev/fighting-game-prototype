using UnityEngine;

public class EnemyChaseCheck : MonoBehaviour
{
	public GameObject PlayerTarget { get; set; }

	[SerializeField] private Enemy _enemy;

	private void Awake()
	{
		PlayerTarget = GameObject.FindGameObjectWithTag("Player");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject == PlayerTarget)
		{
			_enemy.SetChaseStatus(true);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject == PlayerTarget)
		{
			_enemy.SetChaseStatus(false);
		}
	}
}
