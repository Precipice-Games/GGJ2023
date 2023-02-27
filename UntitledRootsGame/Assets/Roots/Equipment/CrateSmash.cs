using UnityEngine;

namespace Roots.Equipment
{
	/// <summary>
	///	Player can destroy some crates on collision. Attach this script to a crate prefab to make it destructible.
	/// </summary>
	public class CrateSmash : MonoBehaviour
	{
		[SerializeField, Tooltip("Prefab representing the broken form of this block")]
		private GameObject fracturedCrate;

		private void OnCollisionEnter(Collision collision)
		{
			if(collision.gameObject.CompareTag("Player"))
			{
				Shatter();
			}
		}

		/// <summary>
		/// Destroys this crate by replacing it with a broken version.
		/// </summary>
		private void Shatter()
		{
			var crateTransform = transform;
			Instantiate(fracturedCrate, crateTransform.position, crateTransform.rotation);
			Destroy(this.gameObject);
		}
	}
}
