using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>Wind force for pushing the player around</summary>
/// <remarks>
/// Possible periodic functions for wind gusts include:
///   1) \frac{\cos\left(2\pi\left(\frac{x}{d}+\frac{1}{2}\right)\right)+1}{2}
///   2) \frac{\cos\left(2\pi x+\pi\right)\ -\ \sin^{2}\left(2\pi x+\pi\right)+\frac{5}{4}}{\frac{9}{4}}
///   3) \frac{\cos\left(2\pi x+\pi\right)\ -\ \sin^{2}\left(4\pi x+\pi\right)+\frac{7}{4}}{\frac{11}{4}}
///  See https://www.desmos.com/calculator to graph these functions.
/// Possible periodic functions for wind direction changes:
///   1) \frac{\cos\left(2\pi\left(\frac{x}{d}+\frac{3}{4}\right)\right)}{2}
///   ...
/// </remarks>
public class Wind : MonoBehaviour
{
	/// <summary>Wind patterns for variety</summary>
	private enum Pattern
	{
		Parallel
		, Turbulent
		, Vortex
	}

	[Tooltip("Direction vector in the X-Y plane"), SerializeField]

	private Vector2 prevailingDirection = Vector2.right;

	[Tooltip("Strength of the prevailing wind"), SerializeField]

	private float prevailingStrength = 1f;

	[Tooltip("Strength of periodic wind gusts"), SerializeField]

	private float gustStrength = 0;

	[Tooltip("How long periodic wind gusts last (in seconds)"), SerializeField]

	private float gustDuration = 1f;

	[Tooltip("Wind may deviate from the prevailing direction by half this angle (in degrees)"), SerializeField]

	private float shiftAngle = 0;

	[Tooltip("How long to complete one cycle of direction changes (in seconds)"), SerializeField]

	private float shiftDuration = 0;

	[Tooltip("Pattern of variability for this wind"), SerializeField]

	private Pattern pattern = Pattern.Parallel;

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
		float deviation = shiftAngle;
		Bounds bounds = GetComponent<Collider>().bounds;
		float relativePosition = position.x / bounds.size.x;
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
			case Pattern.Parallel:
			default:
				newDirection = Direction;
				break;
		}
		return Strength * newDirection;
	}

	public Vector3 Direction
	{
		get
		{
			float shift = shiftAngle * math.cos(2 * math.PI * (Time.time / shiftDuration + 0.75f));
			_currentDirection = Quaternion.Euler(0, 0, shiftAngle) * prevailingDirection;
			return _currentDirection;
		}
	}

	/// <summary>Computes a possibly variable strength of the wind.</summary>
	public float Strength
	{
		get
		{
			float gust = gustStrength * (math.cos(2 * math.PI * (Time.time / gustDuration + 0.5f)) + 1f);
			_currentStrength = prevailingStrength + gust;
			//print(_currentStrength + " " + Time.time);
			return _currentStrength;
		}
	}
}
