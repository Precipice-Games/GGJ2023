using UnityEngine;

namespace Roots.Environment
{
	public class Thorns : MonoBehaviour
	{
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				HurtPlayer(collision.gameObject.GetComponent<BrootusController>());
			}
		}

		private void HurtPlayer(BrootusController player)
		{
			Debug.Log("Dead");
			player.Die();
		}
	}
}
