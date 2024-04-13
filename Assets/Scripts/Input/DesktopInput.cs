using System;
using UnityEngine;

public class DesktopInput : MonoBehaviour, IInput
{
	public event Action LeftAttack;
	public event Action RightAttack;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			LeftAttack?.Invoke();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			RightAttack?.Invoke();
		}
	}
}