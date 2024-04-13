using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings")]
public class EnemySettings : ScriptableObject
{
	public int MaxHealth;

	public AudioClip AttackClip;
	public AudioClip HitClip;
	public AudioClip DeathClip;

	[SerializeField, Min(1)] private float _minMovementSpeed;
	[SerializeField] private float _maxMovementSpeed;
	[SerializeField, Min(1)] private int _minDamage;
	[SerializeField] private int _maxDamage;
	[Tooltip("Эта переменная отвечает за вес, то есть вероятность спавна врага. Желательно делать каждому врагу уникальное значение!")]
	[SerializeField, Range(1, 100)] private int _weight;

	private void OnValidate()
	{
		if(_minMovementSpeed > _maxMovementSpeed)
			_maxMovementSpeed = _minMovementSpeed;

		if (_minDamage > _maxDamage)
			_maxDamage = _minDamage;
	}
	public float GetRandomMovementSpeed()
	{
		return Random.Range(_minMovementSpeed, _maxMovementSpeed);
	}
	public int GetRandomDamagePower()
	{
		return Random.Range(_minDamage, _maxDamage);
	}
	public int GetWeight()
	{
		return _weight;
	}
}
