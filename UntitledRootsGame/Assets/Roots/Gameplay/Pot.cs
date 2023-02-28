using UnityEngine;

namespace Roots.Gameplay
{
	/// <summary>
	/// Player goal is to reach a pot. Attach this script to a GameObject to create a victory location for a level.
	/// </summary>
	public class Pot : MonoBehaviour
	{
		[SerializeField, Tooltip("The highest level currently unlocked"), Range(1, float.PositiveInfinity)]
		private IntValue maxLevel;

		[SerializeField, Tooltip("The level this pot is assigned to"), Range(1, float.PositiveInfinity)]
		private IntValue currentLevel;

		public delegate void VictoryHandler();

		public static event VictoryHandler OnVictory;

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				Debug.Log("Victory");
				IncreaseMaxLevel();
				OnVictory?.Invoke();
			}
		}

		/// <summary>
		/// Completing a level unlocks the next.
		/// </summary>
		private void IncreaseMaxLevel()
		{
			if (maxLevel.Value > currentLevel.Value)
				return;
			maxLevel.Value = currentLevel.Value + 1;
		}
	}
}
