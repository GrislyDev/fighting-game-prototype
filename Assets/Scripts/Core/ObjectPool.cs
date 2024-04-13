using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
	private Dictionary<string, List<Enemy>> _objects;

	public ObjectPool()
	{
		_objects = new Dictionary<string, List<Enemy>>();

	}

	public void AddPrefabToPool(Enemy prefab, int initSize)
	{
		if (!_objects.ContainsKey(prefab.name))
		{
			var list = new List<Enemy>();
			Enemy obj;

			for (int i = 0; i < initSize; i++)
			{
				obj = GameObject.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
				obj.gameObject.SetActive(false);
				list.Add(obj);
			}

			_objects[prefab.name] = list;
		}
	}

	public Enemy GetPoolObject(Enemy prefab)
	{
		if (_objects.ContainsKey(prefab.name))
		{
			Enemy obj;

			var list = _objects[prefab.name];

			for (int i = 0; i < list.Count; i++)
			{
				if (!list[i].gameObject.activeInHierarchy)
				{
					obj = list[i];
					obj.gameObject.SetActive(true);
					return obj;
				}

			}

			obj = GameObject.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
			list.Add(obj);
			return obj;
		}

		throw new System.NullReferenceException($"Object pool does not has {prefab.name}");
	}
}