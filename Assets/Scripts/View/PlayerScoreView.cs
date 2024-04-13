using UnityEngine;
using TMPro;

public class PlayerScoreView : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;
	[SerializeField] private TextMeshProUGUI _scoreText;

	private void Start()
	{
		_scoreText.text = "0";
	}
	private void OnEnable()
	{
		_gameManager.ScoreChanged += ScoreChangedHandler;
	}
	private void OnDisable()
	{
		_gameManager.ScoreChanged -= ScoreChangedHandler;
	}
	private void ScoreChangedHandler(int score)
	{
		_scoreText.text = score.ToString();
	}
}
