using UnityEngine;

public class RollingCharacter : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed;
    private Camera _cam;
    private Vector3 _offset;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = FindObjectOfType<Camera>();
        _offset = _cam.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        _rb.AddForce(movement * speed);
    }

    private void LateUpdate()
    {
        _cam.transform.position = transform.position + _offset;
    }
}