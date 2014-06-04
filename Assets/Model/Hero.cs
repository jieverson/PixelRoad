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

    void Start()
    {

    }

    void Update()
    {
        if (!IsAlive && !blood.isPlaying)
        {
            IsAlive = true;
            transform.position = new Vector3(0, 0.75f, 0);
        }

        if (_fireTimer < fireTime)
        {
            _fireTimer += Time.deltaTime;
        }
        if (IsAlive && !_firing && !Pause.IsPaused && (Input.GetButton("Jump") || Input.touchCount > 0))
        {
            if (_fireTimer >= fireTime)
            {
                _fireTimer = 0;
                Fire();
            }
        }
        if (!(Input.GetButton("Jump") || Input.touchCount > 0))
        {
            _firing = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        var enemy = other.transform.GetComponent<Monster>();
        if (enemy != null)
        {
            Physics.IgnoreCollision(this.collider, other.collider);            
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
        IsAlive = false;
        Score.points = 0;
    }
}
