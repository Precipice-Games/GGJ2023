using Roots.Characters.Brootus;
using Roots.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Roots.UI
{
	/// <summary>
	///	Attach this to one GameObject in each scene to manage start- and end-game events.
	/// </summary>
	/// <remarks>
	/// Currently, we attach this to a prefab SceneController that can be included in each scene.
	/// </remarks>
	public class SceneController : MonoBehaviour
	{
		private const int IndexOffset = 3;
		[SerializeField] private IntValue currentLevel;
		[SerializeField] private IntValue maxLevel;
		public void StartGame() => SceneManager.LoadScene("LevelSelect");

		private void OnEnable()
		{
			Pot.OnVictory += WinGame;
			Controller.OnDead += LoseGame;
		}

		private void OnDisable()
		{
			Pot.OnVictory -= WinGame;
			Controller.OnDead -= LoseGame;
		}

		public void LoadLevel(int index)
		{
			if (index > maxLevel.Value)
				return;
			currentLevel.Value = index;
			SceneManager.LoadScene(index + IndexOffset);
		}

		private void WinGame() => SceneManager.LoadScene("Victory");

		private void LoseGame() => SceneManager.LoadScene("EndGame");

		public void QuitGame() => Application.Quit();
	}
}
