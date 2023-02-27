using UnityEngine;

namespace Roots.MaterialLibrary
{
	/// <summary>
	/// Adds a visual effect for disintegration. Attach this to any GameObject that has a SkinnedMesh.
	/// </summary>
	public class Dissolver : MonoBehaviour
	{
		[SerializeField, Tooltip("Seconds until the dissolving object is destroyed")]
		private float dissolveSpeed;

		private Material _mat;
		private static readonly int s_noiseStrength = Shader.PropertyToID("_NoiseStrength");

		private void Start()
		{
			_mat = GetComponent<SkinnedMeshRenderer>().material;
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Tab))
			{
				_mat.SetFloat(s_noiseStrength, Mathf.MoveTowards(_mat.GetFloat(s_noiseStrength), 1f, dissolveSpeed * Time.deltaTime));
				Destroy(this.gameObject);
			}
		}
	}
}
