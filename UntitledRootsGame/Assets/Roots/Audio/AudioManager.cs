using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioClip _music;
	[SerializeField] private AudioSource _source;
	public static AudioManager Singleton { get; private set; }


	private void Awake()
	{
		DontDestroyOnLoad(this);
		if (Singleton != null)
		{
			Destroy(gameObject);
			return;
		}

		Singleton = this;
	}

	private void Start()
	{
		_source.Play();
	}
}
