using UnityEngine;
using System.Collections;

public class BindScreenRotationToAccelerometer : MonoBehaviour {

    public float speed, minAngle, maxAngle, time;

    public float accelerometerMultiplier = 1.5F;

    private float _timer;

    public GUIText debug;

    void Update () {

        _timer += Time.deltaTime;
        if (_timer >= time)
        {
            _timer -= time;

            var axisX = Input.GetAxis("Horizontal");
            var accelX = Input.acceleration.x;

            var horizontal = axisX != 0 ? axisX : accelX * accelerometerMultiplier * -1;

            var addAngle = speed * horizontal * Time.deltaTime;
            var currentAngle = this.transform.rotation.eulerAngles.z;

            if (currentAngle + addAngle >= 360 + minAngle ||
                currentAngle + addAngle <= maxAngle)
            {
                this.transform.Rotate(new Vector3(0, 0, 1), addAngle);
            }

            //var angle = Input.acceleration.x * maxAngle * -1;
            //this.transform.rotation = Quaternion.identity;
            //this.transform.Rotate(new Vector3(0, 0, 1), angle);

            //debug.text = angle.ToString();
        }
	}
}
