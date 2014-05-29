using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{

    public ParticleSystem blood;
    public float fireTime;
    public Transform fire1;

    private float _fireTimer = float.MaxValue;
    private bool _firing;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!renderer.enabled && !blood.isPlaying)
        {
            renderer.enabled = true;
            transform.position = new Vector3(0, 0.75f, 0);
        }

        if (_fireTimer < fireTime)
        {
            _fireTimer += Time.deltaTime;
        }
        if (renderer.enabled && (Input.GetButton("Jump") || Input.touchCount > 0))
        {
            if (_fireTimer >= fireTime)
            {
                _fireTimer = 0;
                Fire();
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        var enemy = other.transform.GetComponent<Monster>();
        if (enemy != null)
        {
            Destroy(other.gameObject);

            Physics.IgnoreCollision(this.collider, other.collider);
            if (renderer.enabled)
            {
                Death();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var wall = other.transform.GetComponent<Wall>();
        if (wall != null)
        {
            if (renderer.enabled)
            {
                Death();
            }
        }
    }

    private void Fire()
    {
        _firing = true;
        var obj = (Transform)Instantiate(fire1, transform.position + (Vector3.forward * 1), Quaternion.identity);
        obj.GetComponent<AudioSource>().Play();
    }

    private void Death()
    {
        blood.Emit(10);
        blood.audio.Play();
        renderer.enabled = false;
    }
}
