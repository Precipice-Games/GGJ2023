using UnityEngine;

namespace Roots.MaterialLibrary
{
	public class Dissolver : MonoBehaviour
	{
		private Material _mat;
		private bool _hide;
		public float dissolveSpeed;
		public string activateKey;

		private void Start()
		{
			_mat = GetComponent<Renderer>().material;
		}

		private void Update()
		{
			if(Input.GetKeyDown(activateKey))
			{
				_hide = !_hide;
			}

			if(_hide)
			{
				_mat.SetFloat("_NoiseStrength", Mathf.MoveTowards(_mat.GetFloat("_NoiseStrength"), 1f, dissolveSpeed * Time.deltaTime));
			}
			else
			{
				_mat.SetFloat("_NoiseStrength", Mathf.MoveTowards(_mat.GetFloat("_NoiseStrength"), -1f, dissolveSpeed * Time.deltaTime));
			}
		}
	}
}