using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlown : MonoBehaviour
{
    private Rigidbody _rb;

    private GameObject _windZone;

    public bool InWindZone => _windZone != null;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (InWindZone)
        {
            _rb.AddForce(_windZone.GetComponent<Wind>().Strength * _windZone.GetComponent<Wind>().Direction);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wind"))
        {
            _windZone = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wind"))
        {
            _windZone = null;
        }
    }
}
