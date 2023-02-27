using UnityEngine;

namespace Roots.Audio
{
	public class AudioManager : MonoBehaviour
	{
		[SerializeField] private AudioClip music;
		[SerializeField] private AudioSource source;
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
			source.Play();
		}
	}
}
