using UnityEngine;

namespace Roots.Environment.Blocks
{
	/// <summary>
	/// A rotator exhibits a continuous rotation. Attach this script to a tile block to make it spin.
	/// </summary>
	/// <remarks>
	/// By parenting multiple rotators to the same object, it's possible
	/// to create interesting wheel designs by using the <tt>startDepth</tt> of
	/// each rotator to control the distance between them. However, since each
	/// block modifies the transform of the parent object, they cannot rotate in
	/// opposing directions. However, that can be accomplished by simply using two
	/// sets of wheels and carefully choosing position, direction, and speed.
	/// </remarks>
	public class Rotator : MonoBehaviour
	{
		/// <summary>Rotator orientation may be clockwise or anti-clockwise</summary>
		enum Orientation { Clockwise, AntiClockwise }

		[Tooltip("How fast the block will rotate (in degrees per second)"), Range(0, 360)]
		[SerializeField] private float rate = 60;
		[Tooltip("Adjust the initial position of the rotating block"), Range(-10, 10)]
		[SerializeField] private float startDepth;
		[Tooltip("Determines the handedness of the block's rotation")]
		[SerializeField] private Orientation orientation = Orientation.Clockwise;

		private Vector3 _spin;
		private Vector3 _origin;
		private Transform _transform;

		void Start()
		{
			_transform = transform;
			_spin = (orientation == Orientation.Clockwise) ? new Vector3(0, 0, -rate) : new Vector3(0, 0, rate);
			_transform.localPosition += startDepth * _transform.localScale.y * Vector3.right;

		}

		void FixedUpdate()
		{
			_transform.parent.transform.Rotate(_spin * Time.fixedDeltaTime);
		}
	}
}
