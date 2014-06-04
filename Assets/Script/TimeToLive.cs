using UnityEngine;
using System.Collections;

public class TimeToLive : MonoBehaviour {

    public float timeToLive;

    private float _count;

	void Update () {
        _count += Time.deltaTime;

        if (_count >= timeToLive)
        {
            Destroy(this.gameObject);
        }
	}
}
