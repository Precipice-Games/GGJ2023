using UnityEngine;
using UnityEngine.UI;

public class UIPlayerWidget : MonoBehaviour
{
    [SerializeField]
    private Image _planet;
    [SerializeField]
    private Image _clouds;
    [SerializeField]
    private RawImage _character;
    [SerializeField]
    private float speed, height;

    private float charPos;

    private void Start()
    {
        charPos = _character.rectTransform.position.y;
    }

    private void Update()
    {
        _planet.transform.Rotate(0,0,0.01f);
        _clouds.transform.Rotate(0,0,0.005f);

        _character.rectTransform.position = new Vector3
            (_character.rectTransform.position.x, Mathf.Sin(Time.time * speed) * height + charPos, _character.rectTransform.position.z);
    }
}