using UnityEngine;

public class SkyboxRotate : MonoBehaviour
{
	public float rotateSpeed;
    void Update()
    {
		RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
    }
}
