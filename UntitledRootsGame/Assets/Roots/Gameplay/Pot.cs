using UnityEngine;

namespace Roots.Gameplay
{
	public class Pot : MonoBehaviour
	{
		[SerializeField] private IntValue _maxLevel;
		[SerializeField] private IntValue _currentLevel;

		public delegate void VictoryHandler();

		public static event VictoryHandler OnVictory;
		// Start is called before the first frame update
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				Debug.Log("Victory");
				IncreaseMaxLevel();
			}
		}

		private void IncreaseMaxLevel()
		{
			if (_maxLevel.Value > _currentLevel.Value)
				return;
			_maxLevel.Value = _currentLevel.Value + 1;
			OnVictory?.Invoke();
		}
	}
}
