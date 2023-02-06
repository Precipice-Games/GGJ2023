using UnityEngine;

namespace Roots.MaterialLibrary
{
	public class Dissolver : MonoBehaviour
	{
		private Material _mat;
		public float dissolveSpeed;

		private void Start()
		{
			_mat = GetComponent<SkinnedMeshRenderer>().material;
		}

		public void Update()
		{
			if(Input.GetKeyDown(KeyCode.Tab))
			{
				_mat.SetFloat("_NoiseStrength", Mathf.MoveTowards(_mat.GetFloat("_NoiseStrength"), 1f, dissolveSpeed * Time.deltaTime));
				Destroy(this.gameObject);
			}
		}
	}
}
