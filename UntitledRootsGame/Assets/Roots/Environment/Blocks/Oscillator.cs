using Unity.Mathematics;
using UnityEngine;

namespace Roots.Environment.Blocks
{
	/// <summary>
	/// An oscillator exhibits a periodic, linear motion. Attach this script to a tile block to make it oscillate.
	/// </summary>
	public class Oscillator : MonoBehaviour
	{
		/// <summary>Oscillator orientation may be horizontal or vertical</summary>
		enum Orientation { Horizontal, Vertical }

		[Tooltip("How far the block will move in local space"), SerializeField, Range(0, 1000)]
		private float amplitude = 4;
		[Tooltip("How many cycles the block completes in a second"), SerializeField, Range(0.01f, 100)]
		private float frequency = 1;
		[Tooltip("Adjust the initial position of the oscillating block"), SerializeField, Range(-10, 10)]
		private float startDepth;
		[Tooltip("Determines the local axis along which the block moves"), SerializeField]
		private Orientation orientation = Orientation.Vertical;

		private Vector3 _direction;
		private Vector3 _origin;
		private Transform _transform;

		void Start()
		{
			_transform = transform;
			_direction = orientation == Orientation.Vertical ? Vector3.up : Vector3.right;
			_origin = _transform.localPosition + startDepth * _transform.localScale.y * _direction;
		}

		void FixedUpdate()
		{
			_transform.localPosition = _origin + amplitude * _transform.localScale.y * math.sin(frequency * Time.time) * _direction;
		}
	}
}
