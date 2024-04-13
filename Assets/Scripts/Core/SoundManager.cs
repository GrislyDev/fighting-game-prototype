using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	[SerializeField] private AudioSource _musicSource;
	[SerializeField] private AudioSource _soundSource;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}
	}
	public void PlayMusic()
	{
		_musicSource.Play();
	}
	public void PauseMusic()
	{
		_musicSource.Pause();
	}
	public void UnpauseMusic()
	{
		_musicSource.UnPause();
	}
	public void StopMusic()
	{
		_musicSource.Stop();
	}
	public void PlaySound(AudioClip audioClip)
	{
		_soundSource.PlayOneShot(audioClip);
	}
}