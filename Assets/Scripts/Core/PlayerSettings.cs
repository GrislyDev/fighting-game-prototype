using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings")]
public class PlayerSettings : ScriptableObject
{	
	public int MaxHealth;
	public float AttackRange;

	public AudioClip AttackClip;
	public AudioClip HitClip;
	public AudioClip DeathClip;

	[SerializeField, Min(1)] private int _minDamage;
	[SerializeField] private int _maxDamage;

	private void OnValidate()
	{
		if (_minDamage > _maxDamage)
			_maxDamage = _minDamage;
	}

	public int GetRandomDamagePower()
	{
		return Random.Range(_minDamage, _maxDamage);
	}
}
