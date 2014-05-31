using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{

    public ParticleSystem blood;
    public Transform bloodExplosion;
    public float fireTime;
    public Transform fire1;

    private float _fireTimer = float.MaxValue;
    private bool _firing;

    public bool IsAlive
    {
        get
        {
            return this.GetComponent<SpriteRenderer>().renderer.enabled;
        }
        set
        {
            this.GetComponent<SpriteRenderer>().renderer.enabled = value;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (!renderer.enabled && !blood.isPlaying)
        if (!IsAlive && !blood.isPlaying)
        {
            //renderer.enabled = true;
            IsAlive = true;
            transform.position = new Vector3(0, 0.75f, 0);
        }

        if (_fireTimer < fireTime)
        {
            _fireTimer += Time.deltaTime;
        }
        //if (renderer.enabled && (Input.GetButton("Jump") || Input.touchCount > 0))
        if (IsAlive && (Input.GetButton("Jump") || Input.touchCount > 0))
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
            Physics.IgnoreCollision(this.collider, other.collider);
            //if (renderer.enabled)
            if(IsAlive)
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
            //if (renderer.enabled)
            if (IsAlive)
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
        blood.audio.Play();
        Instantiate(bloodExplosion, transform.position, Quaternion.identity);
        blood.Emit(10);
        //renderer.enabled = false;
        IsAlive = false;
        Score.points = 0;
    }
}
