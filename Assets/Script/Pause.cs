using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    private bool _pause;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pause = !_pause;
            Time.timeScale = _pause ? 0 : 1;
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage(_pause ? "OnPauseGame" : "OnResumeGame", SendMessageOptions.DontRequireReceiver);
            }
        }
	}
}
