using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    public Transform bloodExplosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        var fire = other.transform.GetComponent<Fire>();
        if (fire != null)
        {
            Destroy(other.gameObject);
            Death();
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

    private void Death()
    {
        Score.points += 10;
        Instantiate(bloodExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
