using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

    public Transform bloodExplosion;
    public SpriteRenderer currentSprite, deadSprite;

	void Start () {
        deadSprite.renderer.enabled = false;
	}
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        var fire = other.transform.GetComponent<Fire>();
        if (fire != null)
        {
            Destroy(other.gameObject);

            if (currentSprite.renderer.enabled)
            {
                Damage();
            }
            else
            {
                Death();
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        var hero = other.transform.GetComponent<Hero>();
        if (hero != null)
        {
            Death();
        }
        else
        {
            var road = other.transform.GetComponent<Road>();
            if (road != null)
            {
                Physics.IgnoreCollision(this.collider, other.collider);
            }
        }
    }

    private void Damage()
    {
        currentSprite.renderer.enabled = false;
        deadSprite.renderer.enabled = true;
        Instantiate(bloodExplosion, transform.position, Quaternion.identity);
    }

    private void Death()
    {
        Score.points += 20;
        Instantiate(bloodExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
