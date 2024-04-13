using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
	[SerializeField] private Image _panelImage;
	[SerializeField] private TextMeshProUGUI _gameOverText;
	[SerializeField] private TextMeshProUGUI _finalScoreText;
	[SerializeField] private Button _restartButton;
	[SerializeField] private Button _menuButton;

	private const string GAME_SCENE = "Game";
	private const string MENU_SCENE = "Menu";

	private Tween _scaleGameOverTween;
	private Tween _panelImageTransparentTween;
	private Color32 _panelColor = new Color32(75, 75, 75, 200);
	private float _panelTransparentTime = 1f;

	private void OnEnable()
	{
		_restartButton.onClick.AddListener(RestartGame);
		_menuButton.onClick.AddListener(ReturnToMainMenu);
	}
	private void OnDisable()
	{
		_restartButton.onClick.RemoveAllListeners();
		_menuButton.onClick.RemoveAllListeners();
	}
	public void OpenGameOverPanel(int finalScore)
	{
		_finalScoreText.text = $"Score: {finalScore.ToString()}";

		gameObject.SetActive(true);
		ScaleGameOverText();
		ChangePanelImageTransparent();
	}
	private void RestartGame()
	{
		SceneManager.LoadScene(GAME_SCENE);
	}
	private void ReturnToMainMenu()
	{
		SceneManager.LoadScene(MENU_SCENE);
	}
	private void ScaleGameOverText()
	{
		_scaleGameOverTween?.Kill();
		_scaleGameOverTween = _gameOverText.transform.DOScale(0, 0);
		_scaleGameOverTween = _gameOverText.transform.DOScale(1f, 1f);
	}
	private void ChangePanelImageTransparent()
	{
		_panelImageTransparentTween?.Kill();
		_panelImageTransparentTween = _panelImage.DOColor(_panelColor, _panelTransparentTime);
	}
}
