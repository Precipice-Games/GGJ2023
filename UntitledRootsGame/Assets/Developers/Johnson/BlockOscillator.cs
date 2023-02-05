using Unity.Mathematics;
using UnityEngine;

/// <summary>An oscillator exhibits a periodic, linear motion.</summary>
public class BlockOscillator : MonoBehaviour
{
    /// <summary>Oscillator orientation may be horizontal or vertical</summary>
    enum Orientation { HORIZONTAL, VERTICAL }

    [Tooltip("How far the block will move in local space")]
    [SerializeField] private float amplitude = 4;
    [Tooltip("How many cycles the block completes in a second")]
    [SerializeField] private float frequency = 1;
    [Tooltip("Adjust the initial position of the oscillating block")]
    [SerializeField] private float startDepth = 0;
    [Tooltip("Determines the local axis along which the block moves")]
    [SerializeField] private Orientation orientation = Orientation.VERTICAL;

    private Vector3 _direction;
    private Vector3 _origin;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _direction = orientation == Orientation.VERTICAL ? Vector3.up : Vector3.right;
        _origin = _transform.localPosition + startDepth * _transform.localScale.y * _direction;
    }

    // Frame-rate independent update for physics calculations.
    void FixedUpdate()
    {
        _transform.localPosition = _origin + amplitude * _transform.localScale.y * math.sin(frequency * Time.time) * _direction;
    }
}
