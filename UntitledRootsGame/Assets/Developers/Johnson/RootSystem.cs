using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class RootSystem : MonoBehaviour
{
    private const float RootedMass = 1000f;
    private const float RootedAngularDrag = 20f;
    private const float UnrootedMass = 0.5f;
    private const float UnrootedAngularDrag = 1f;
    private Transform _transform;
    private Rigidbody _ground;
    private CapsuleCollider _root;
    private SpringJoint _rootcap = null;

    void Start()
    {
        _transform = transform;
        _root = (from c in GetComponents<CapsuleCollider>() where c.isTrigger select c).First();
        Assert.IsNotNull(_root);
        _root.enabled = false;
    }

    void Update()
    {
        
    }

    public bool IsRooted() => _root.enabled;

    internal void Root()
    {
        // extends the root mesh out beyond the seed body mesh
        //transform.Find("BrootusRoot").transform.Translate(Vector3.up * _transform.localScale.y, relativeTo: _transform);
        // extends the root trigger-collider out just beyond the tip of the seed body collider
        _root.center += 0.3f * Vector3.up;
        _root.enabled = true;
    }

    internal void Unroot()
    {
        _root.center += 0.3f * Vector3.down;
        _root.enabled = false;
        GetComponent<Rigidbody>().ResetCenterOfMass();
        GetComponent<Rigidbody>().mass = UnrootedMass;
        GetComponent<Rigidbody>().angularDrag = UnrootedAngularDrag;

        //_transform.Find("BrootusRoot").transform
            //.Translate(Vector3.down * _transform.localScale.y, relativeTo: _transform);
        Destroy(_rootcap);
        _rootcap = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_rootcap == null && other.gameObject.CompareTag("Ground"))
        {
            _ground = other.gameObject.GetComponent<Rigidbody>();
            GetComponent<Rigidbody>().centerOfMass = _root.center;
            GetComponent<Rigidbody>().mass = RootedMass;
            GetComponent<Rigidbody>().angularDrag = RootedAngularDrag;
            _rootcap = this.gameObject.AddComponent<SpringJoint>();
            _rootcap.connectedBody = _ground;
            _rootcap.anchor = other.ClosestPoint(_transform.position) - _transform.position;
            // TODO make these constants or fields?
            _rootcap.spring = 1000000f;
            _rootcap.damper = 0.01f;
            _rootcap.tolerance = 0.001f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_ground && _ground.gameObject == other.gameObject)
        {
            _ground = null;
        }
    }
}
