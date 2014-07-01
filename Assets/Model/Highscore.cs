using UnityEngine;
using System.Collections;

public class Highscore : MonoBehaviour {

	void Update () {
        if (Input.GetButton("Jump") || Input.touchCount > 0)
        {
            Application.LoadLevel(0);
        }
	}
}
