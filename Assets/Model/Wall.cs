using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

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
        }
    }
}
