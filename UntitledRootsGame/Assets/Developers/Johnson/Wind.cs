using System;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

/// <summary>Wind force for pushing the player around</summary>
public class Wind : MonoBehaviour
{
    /// <summary>Wind patterns for variety</summary>
    enum Pattern { Parallel, Wave, Turbulent, Vortex }

    [Tooltip("Direction vector in the X-Y plane")]
    [SerializeField] private Vector2 prevailingDirection = Vector2.right;

    [Tooltip("Strength multiplier for this wind object")]
    [SerializeField] private float prevailingStrength = 1f;

    [Tooltip("Wind may fluctuate from the prevailing strength by half this amount")]
    [SerializeField] private float gusts = 0f;

    [Tooltip("Wind may deviate from the prevailing direction by half this angle (in degrees)")]
    [SerializeField] private float spread = 0f;

    [Tooltip("Wind may fluctuate from the prevailing strength by half this amount")]
    [SerializeField] private Pattern pattern = Pattern.Parallel;

    private Vector2 _currentDirection;
    private float _currentStrength;

    private void Start()
    {
        prevailingDirection.Normalize();
        _currentDirection = prevailingDirection;
        _currentStrength = prevailingStrength;
    }

    /// <summary>Computes the wind force applied at a given position within a wind zone.</summary>
    /// <param name="position">the position of an object inside the wind zone</param>
    /// <returns>the magnitude and direction of the wind force</returns>
    public Vector3 Force(Vector3 position)
    {
        Vector3 newDirection;
        var deviation = spread;
        var bounds = GetComponent<Collider>().bounds;
        var relativePosition = position.x / bounds.size.x;
        switch (pattern)
        {
            case Pattern.Vortex:
                deviation = Vector2.SignedAngle(prevailingDirection, Vector2.up) >= 0 ? -90 : 90;
                _currentDirection = position - transform.position;
                newDirection = Quaternion.Euler(0, 0, deviation) * _currentDirection;
                break;
            case Pattern.Turbulent:
                deviation *= Mathf.PerlinNoise(position.x, position.y) - 0.5f;
                newDirection = Quaternion.Euler(0, 0, deviation) * _currentDirection;
                _currentDirection = newDirection;
                break;
            case Pattern.Wave:
                deviation *= math.sin(10f * relativePosition);
                newDirection = Quaternion.Euler(0, 0, deviation) * _currentDirection;
                break;
            case Pattern.Parallel:
            default:
                deviation *= spread * (Random.value - 0.5f);
                _currentDirection = Quaternion.Euler(0, 0, deviation) * _currentDirection;
                newDirection = _currentDirection;
                break;
        }
        return Strength * newDirection;
    }

    /// <summary>Computes a possibly variable strength of the wind.</summary>
    public float Strength
    {
        get
        {
            _currentStrength = (1f + gusts * (Random.value - 0.5f)) * (prevailingStrength + _currentStrength) / 2f;
            return _currentStrength;
        }
    }
}
