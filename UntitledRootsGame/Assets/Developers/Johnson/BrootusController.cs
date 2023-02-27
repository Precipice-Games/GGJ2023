using UnityEngine;

public class BrootusController : MonoBehaviour
{
	[Tooltip("Provide a prefab for the pappus Brootus may sprout."), SerializeField]
	private GameObject pappusPrefab;

	private GameObject _pappus;

	[field: SerializeField] public float KillPlane { get; private set; }
	public delegate void DeadHandler();

	public static event DeadHandler OnDead;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S))
		{
			var roots = GetComponent<RootSystem>();
			if (roots.HasRoots())
				roots.Uproot();
			else
				roots.GrowRoot();
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
