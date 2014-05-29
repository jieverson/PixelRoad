using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    public float timeToScore;

    public static int points;

    private float _timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;
        if (_timer >= timeToScore)
        {
            _timer -= timeToScore;
            points += 1;
        }

        var gui = this.gameObject.GetComponent<GUIText>();
        gui.text = points.ToString();
	}
}
