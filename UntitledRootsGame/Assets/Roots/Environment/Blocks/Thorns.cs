using Roots.Characters.Brootus;
using UnityEngine;

namespace Roots.Environment.Blocks
{
	/// <summary>
	/// Thorns harm the player. Attach this script to a GameObject to create a hazard.
	/// </summary>
	public class Thorns : MonoBehaviour
	{
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				HurtPlayer(collision.gameObject.GetComponent<Controller>());
			}
		}

		/// <summary>
		///	Currently, the player dies on contact (one-shot).
		/// </summary>
		/// <remarks>
		/// \todo Revisit this from a design perspective.
		/// </remarks>
		/// <param name="player">reference to the player <c>Controller</c> script</param>
		private void HurtPlayer(Controller player)
		{
			Debug.Log("Dead");
			player.Die();
		}
	}
}
