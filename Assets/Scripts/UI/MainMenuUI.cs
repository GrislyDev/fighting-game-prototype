using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private Button _playButton;

	private const string GAME_SCENE = "Game";

	private void OnEnable()
	{
		_playButton.onClick.AddListener(StartGame);
	}
	private void OnDisable()
	{
		_playButton.onClick.RemoveAllListeners();
	}
	private void StartGame()
	{
		SceneManager.LoadScene(GAME_SCENE);
	}
}
