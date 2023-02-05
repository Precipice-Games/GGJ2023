using UnityEngine;

namespace Roots.Environment
{
	public class Thorns : MonoBehaviour
	{
		public delegate void DeadHandler();

		public static event DeadHandler OnDead;

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				Debug.Log("Dead");
				OnDead?.Invoke();
			}
		}
	}
}
