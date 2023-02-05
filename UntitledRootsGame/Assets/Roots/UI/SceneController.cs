using Roots.Environment;
using Roots.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Roots.UI
{
	public class SceneController : MonoBehaviour
	{
		private const int IndexOffset = 3;
		[SerializeField] private IntValue _currentLevel;
		[SerializeField] private IntValue _maxLevel;
		public void StartGame() => SceneManager.LoadScene("LevelSelect");

		private void OnEnable()
		{
			Pot.OnVictory += WinGame;
			Thorns.OnDead += LoseGame;
		}

		private void OnDisable()
		{
			Pot.OnVictory -= WinGame;
			Thorns.OnDead -= LoseGame;
		}

		public void LoadLevel(int index)
		{
			if (index > _maxLevel.Value)
				return;
			_currentLevel.Value = index;
			SceneManager.LoadScene(index + IndexOffset);
		}

		private void WinGame() => SceneManager.LoadScene("Victory");

		private void LoseGame() => SceneManager.LoadScene("EndGame");

		public void QuitGame() => Application.Quit();
	}
}
