using UnityEngine;
using System.Collections;

public class Chooser : MonoBehaviour {

    public Transform[] prefabs;

	// Use this for initialization
	void Start () {
        var index = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[index]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
