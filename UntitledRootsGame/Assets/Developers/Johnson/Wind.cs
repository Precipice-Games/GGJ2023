using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [Tooltip("Direction vector in the X-Y plane")]
    [SerializeField] private Vector2 prevailingDirection = Vector2.zero;

    [Tooltip("Strength multiplier for this wind object")]
    [SerializeField] private float prevailingStrength = 1f;

    [Tooltip("Wind may deviate from the prevailing direction by half this angle (in degrees)")]
    [SerializeField] private float spread = 0f;

    [Tooltip("Wind may fluctuate from the prevailing strength by half this amount")]
    [SerializeField] private float flux = 0f;

    public float Strength => prevailingStrength + flux * (Random.value - 0.5f) * prevailingStrength;

    public Vector3 Direction => Quaternion.Euler(0, 0, spread * (Random.value - 0.5f)) * prevailingDirection;
}
