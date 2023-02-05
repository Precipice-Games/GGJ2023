using UnityEngine;

namespace Roots.Gameplay
{
	public class GameState : MonoBehaviour
	{
		public static GameState Singleton { get; private set; }
		[SerializeField] private IntValue _maxLevel;
		[SerializeField] private IntValue _currentLevel;

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
	}
}
