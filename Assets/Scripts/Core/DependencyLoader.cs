using UnityEngine;

public class DependencyLoader : MonoBehaviour
{
	[SerializeField] private Player _player;
	[SerializeField] private DesktopInput _desktopInput;

	private void Awake()
	{
		_player.Initialize();
	}
}
