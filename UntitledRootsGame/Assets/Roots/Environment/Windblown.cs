using UnityEngine;

namespace Roots.Environment
{
	/// <summary>Governs the behavior of objects susceptible to wind.</summary>
	/// <remarks>
	/// Attach this script to a GameObject to make it susceptible to the wind; the
	/// GameObject **must** have an attached Rigidbody component. Wind zones are
	/// GameObjects with both the "Wind" tag and an attached Wind script.
	/// </remarks>
	public class Windblown : MonoBehaviour
	{
		/// <summary>Used for objects that should be carried up by the wind (e.g., Brootus' pappus).</summary>
		[SerializeField, Tooltip("TBD"), Range(0, 10)] private float lift;

		/// <summary>Reference to wind currently affecting this object.</summary>
		private Wind _windZone;

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
				var windForce = _windZone.Force(transform.position);
				_rb.AddForce(windForce + lift * windForce.magnitude * Vector3.up);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			var wind = other.GetComponent<Wind>();
			if (wind)
			{
				_windZone = wind;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.GetComponent<Wind>() == _windZone)
			{
				_windZone = null;
			}
		}
	}
}
