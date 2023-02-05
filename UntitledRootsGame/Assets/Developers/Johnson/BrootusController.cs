using UnityEngine;

public class BrootusController : MonoBehaviour
{
	[field: SerializeField] public float KillPlane { get; private set; }
	public delegate void DeadHandler();

	public static event DeadHandler OnDead;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			var roots = GetComponent<RootSystem>();
			if (roots.HasRoots())
				roots.Uproot();
			else
				roots.GrowRoot();
		}

		if(transform.position.y < KillPlane)
			Die();
	}

	public void Die()
	{
		OnDead?.Invoke();
	}
}
