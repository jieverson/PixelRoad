using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    public float timeToLive = 1;
    
    private float _timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;
        if (_timer >= timeToLive)
        {
            Destroy(this.gameObject);
        }
	}

}
