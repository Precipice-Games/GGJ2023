using UnityEngine;

namespace Roots.Environment
{
	/// <summary>
	/// Provides a moving sky effect. Attach this to the main camera of the scene.
	/// </summary>
	public class SkyboxRotate : MonoBehaviour
	{
		[SerializeField, Tooltip("Rotation speed in degrees per second.")]
		private float rotateSpeed;

		private static readonly int s_rotation = Shader.PropertyToID("_Rotation");

		void Update()
		{
			RenderSettings.skybox.SetFloat(s_rotation, Time.time * rotateSpeed);
		}
	}
}
