using UnityEngine;

public class BrootusController : MonoBehaviour
{
	private void Start()
	{
	}

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
	}
}
