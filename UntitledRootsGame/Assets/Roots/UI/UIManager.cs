using UnityEngine;
using UnityEngine.SceneManagement;

namespace Roots.UI
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private GameObject pauseMenu;

		private bool _isPaused;

		private void Awake()
		{
			_isPaused = false;
			Time.timeScale = 1.0f;
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				PauseResume();
			}
		}

		public void PauseResume()
		{
			_isPaused = !_isPaused;

			if(_isPaused)
			{
				Time.timeScale = 0.0f;
				pauseMenu.SetActive(true);
			}
			else
			{
				Time.timeScale = 1.0f;
				pauseMenu.SetActive(false);
			}
		}

		public void RestartLevel()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void ReturnToMenu()
		{
			SceneManager.LoadScene("Menu");
		}
	}
}
