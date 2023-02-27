using UnityEngine;

namespace Roots.Characters.Brootus
{
	/// <summary>
	/// Provides for the mechanics of the player character, Brootus.
	/// </summary>
	/// <remarks>
	/// Attach this script to the player character prefab.
	///	|Dependencies|References|
	/// |------------|----------|
	/// |RootSystem  |Player    |
	/// </remarks>
	public class Controller : MonoBehaviour
	{
		[field: SerializeField] private float KillPlane { get; set; }

		internal delegate void DeadHandler();

		internal static event DeadHandler OnDead;

		/// <summary>
		/// Brootus instantiates this object when sprouting a pappus; must provide a prefab in the Inspector.
		/// </summary>
		[Tooltip("Provide a prefab for the pappus Brootus may sprout."), SerializeField]
		private GameObject pappusPrefab;

		private GameObject _pappus;
		private RootSystem _roots;

		private void Start()
		{
			_roots = GetComponent<RootSystem>();
		}

		/// <summary>
		/// Update the player state each frame.
		/// </summary>
		/// More info...
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S))
			{
				if (_roots.HasRoots())
					_roots.Uproot();
				else
					_roots.GrowRoot();
			}
			else if (Input.GetKeyDown(KeyCode.W))
			{
				if (_pappus)
				{
					// TODO destroy the pappus later (via timer or off-screen-detection?)
					Destroy(_pappus.GetComponent<Joint>());
					_pappus.transform.parent = null;
					_pappus = null;
				}
				else
				{
					_pappus = Instantiate(pappusPrefab, transform, false);
					_pappus.GetComponent<Joint>().connectedBody = GetComponent<Rigidbody>();
					_pappus.SetActive(true);
				}
			}

			if(transform.position.y < KillPlane)
				Die();
		}

		public void Die()
		{
			OnDead?.Invoke();
		}
	}
}
