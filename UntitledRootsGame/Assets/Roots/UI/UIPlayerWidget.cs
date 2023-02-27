using UnityEngine;
using UnityEngine.UI;

namespace Roots.UI
{
	public class UIPlayerWidget : MonoBehaviour
	{
		[SerializeField]
		private Image planet;
		[SerializeField]
		private Image clouds;
		[SerializeField]
		private RawImage character;
		[SerializeField]
		private float speed, height;

		private float _charPos;

		private void Start()
		{
			_charPos = character.rectTransform.position.y;
		}

		private void Update()
		{
			planet.transform.Rotate(0,0,0.01f);
			clouds.transform.Rotate(0,0,0.005f);

			var position = character.rectTransform.position;
			position = new Vector3
				(position.x, Mathf.Sin(Time.time * speed) * height + _charPos, position.z);
			character.rectTransform.position = position;
		}
	}
}
