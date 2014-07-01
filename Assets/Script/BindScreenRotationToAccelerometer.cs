using UnityEngine;
using System.Collections;

public class BindScreenRotationToAccelerometer : MonoBehaviour {

    public float speed, minAngle, maxAngle, time;

    private float _timer;

    void Update () {

        _timer += Time.deltaTime;
        if (_timer >= time)
        {
            _timer -= time;

            var accelX = Input.GetAxis("Horizontal");
            //var accelX = Input.acceleration.x;

            var addAngle = speed * accelX * Time.deltaTime;
            var currentAngle = this.transform.rotation.eulerAngles.z;
            Debug.Log(this.transform.rotation.eulerAngles.z);
            if (currentAngle + addAngle >= 360 + minAngle ||
                currentAngle + addAngle <= maxAngle)
            {
                this.transform.Rotate(new Vector3(0, 0, 1), addAngle);
            }
        }
	}
}
