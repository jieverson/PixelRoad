using UnityEngine;
using System.Collections;

public class Velocity : MonoBehaviour {

    public Vector3 rigidbodyVelocity;

    public static float Speed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Speed += Time.deltaTime * 0.001F;

        rigidbody.velocity = rigidbodyVelocity * Speed;
	}
}
