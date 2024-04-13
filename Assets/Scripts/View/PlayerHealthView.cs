using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
	[SerializeField] private Player _player;
	[SerializeField] private Slider _healthBar;

	private void Start()
	{
		_healthBar.maxValue = _player.MaxHealth;
		_healthBar.value = _player.MaxHealth;
	}
	private void OnEnable()
	{
		_player.HealthChanged += HealthChangedHandler;
	}
	private void OnDisable()
	{
		_player.HealthChanged -= HealthChangedHandler;
	}
	private void HealthChangedHandler(int currentHealth)
	{
		_healthBar.value = currentHealth;
	}
}