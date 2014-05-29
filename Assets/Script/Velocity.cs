using UnityEngine;
using System.Collections;

public class Velocity : MonoBehaviour {

    public Vector3 rigidbodyVelocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rigidbody.velocity = rigidbodyVelocity;
	}
}
