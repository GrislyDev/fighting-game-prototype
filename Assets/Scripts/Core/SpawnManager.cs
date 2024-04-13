using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private Enemy[] _enemies;
	[SerializeField] private Transform[] _spawnPoints;

	private ObjectPool _objectPool;
	private int _poolSize;
	private int _totalWeight;

	private void Awake()
	{
		CreateObjectPool();
	}

	private void CreateObjectPool()
	{
		_objectPool = new ObjectPool();
		_poolSize = 5;

		for (int i = 0; i < _enemies.Length; i++)
		{
			_objectPool.AddPrefabToPool(_enemies[i], _poolSize);
			_totalWeight += _enemies[i].Settings.GetWeight();
		}
	}

	public void SpawnRandomEnemyAtRandomPosition()
	{
		int enemyIndex = GetRandomIndexByWeight();
		int randomPositionIndex = Random.Range(0, _spawnPoints.Length);

		SpawnEnemy(_enemies[enemyIndex], _spawnPoints[randomPositionIndex].position);
	}
	public void SpawnRandomEnemy(Vector3 spawnPoint)
	{
		int enemyIndex = GetRandomIndexByWeight();

		SpawnEnemy(_enemies[enemyIndex], spawnPoint);
	}
	private void SpawnEnemy(Enemy enemy, Vector3 spawnPoint)
	{
		var obj = _objectPool.GetPoolObject(enemy);
		obj.transform.position = spawnPoint;
	}
	private int GetRandomIndexByWeight()
	{
		int randomWeight = Random.Range(0, _totalWeight);

		for (int index = 0; index < _enemies.Length; index++)
		{
			if (randomWeight <= _enemies[index].Settings.GetWeight())
			{
				return index;
			}
			else
			{
				randomWeight -= _enemies[index].Settings.GetWeight();
			}
		}
		return 0;
	}
}
