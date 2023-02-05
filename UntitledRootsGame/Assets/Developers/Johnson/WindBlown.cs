using UnityEngine;

/// <summary>Governs the behavior of objects susceptible to wind.</summary>
public class WindBlown : MonoBehaviour
{
    /// <summary>Reference to wind currently affecting this object.</summary>
    internal Wind _windZone;

    private Rigidbody _rb;

    /// <summary>Checks whether this object is under the influence of wind.</summary>
    public bool InWindZone => _windZone;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (InWindZone)
        {
            _rb.velocity = 0.25f * _rb.velocity;
            _rb.AddForce(_windZone.Force(transform.position));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            _windZone = other.GetComponent<Wind>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            _windZone = null;
        }
    }
}
