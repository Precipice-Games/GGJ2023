using UnityEngine;

/// <summary>A rotator exhibits a continuous rotation.</summary>
/// <remarks>By parenting multiple rotators to the same object, it's possible
/// to create interesting wheel designs by using the <tt>startDepth</tt> of
/// each rotator to control the distance between them. However, since each
/// block modifies the transform of the parent object, they cannot rotate in
/// opposing directions. However, that can be accomplished by simply using two
/// sets of wheels and carefully choosing position, direction, and speed.</remarks>
public class BlockRotator : MonoBehaviour
{
    /// <summary>Oscillator orientation may be horizontal or vertical</summary>
    enum Orientation { CLOCKWISE, ANTICLOCKWISE }

    [Tooltip("How fast the block will rotate")]
    [SerializeField] private float rate = 1;
    [Tooltip("Adjust the initial position of the rotating block")]
    [SerializeField] private float startDepth = 0;
    [Tooltip("Determines the handedness of the block's rotation")]
    [SerializeField] private Orientation orientation = Orientation.CLOCKWISE;

    private Vector3 _spin;
    private Vector3 _origin;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _spin = (orientation == Orientation.CLOCKWISE) ? new Vector3(0, 0, -rate) : new Vector3(0, 0, rate);
        _transform.localPosition += startDepth * _transform.localScale.y * Vector3.right;

    }

    // Frame-rate independent update for physics calculations.
    void FixedUpdate()
    {
        _transform.parent.transform.Rotate(_spin);
    }
}
