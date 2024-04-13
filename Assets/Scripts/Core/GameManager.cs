using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	
	public static GameManager Instance { get; private set; }

	public event Action<int> ScoreChanged;
	public bool IsGameOn { get; private set; } = true;

	[SerializeField] private GameOverPanel _gameOverPanel;
	[SerializeField] private SpawnManager _spawnManager;

	private int _score = 0;
	private float _minSpawnDelay = 1f;
	private float _maxSpawnDelay = 5f;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}
	private void Start()
	{
		StartCoroutine(SpawnEnemies());
		SoundManager.Instance.UnpauseMusic();
	}
	public void IncrementScore()
	{
		_score++;
		ScoreChanged?.Invoke(_score);
	}
	public void FinishGame()
	{
		IsGameOn = false;
		_gameOverPanel.OpenGameOverPanel(_score);
		SoundManager.Instance.PauseMusic();
	}

	private IEnumerator SpawnEnemies()
	{
		WaitForSeconds delay;

		while (IsGameOn)
		{
			delay = new WaitForSeconds(UnityEngine.Random.Range(_minSpawnDelay, _maxSpawnDelay));
			
			_spawnManager.SpawnRandomEnemyAtRandomPosition();
			yield return delay;

			delay = new WaitForSeconds(UnityEngine.Random.Range(_minSpawnDelay, _maxSpawnDelay));

			_spawnManager.SpawnRandomEnemyAtRandomPosition();
			_spawnManager.SpawnRandomEnemyAtRandomPosition();
			yield return delay;
		}
	}
}
