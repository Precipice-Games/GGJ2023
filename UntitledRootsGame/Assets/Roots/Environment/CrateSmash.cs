using UnityEngine;

public class CrateSmash : MonoBehaviour
{
    [SerializeField] private GameObject _fracturedCrate;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Shatter();
        }
    }

    private void Shatter()
    {
        Instantiate(_fracturedCrate, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}