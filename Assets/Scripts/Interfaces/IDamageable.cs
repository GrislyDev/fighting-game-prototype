using UnityEngine;

public interface IDamageable
{
	int MaxHealth { get; set; }
	int CurrentHealth { get; set; }
	void Damage(int damageAmount);
}