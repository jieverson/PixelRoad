using UnityEngine;
using System.Collections;

public class TranslateControl : MonoBehaviour {

    public float speedX = 5;
    public float speedY = 5;

    public float minX = float.MinValue;
    public float maxX = float.MaxValue;

    public float minY = float.MinValue;
    public float maxY = float.MaxValue;

    public float accelMult = 1;
	
	void Update () {
        var axisX = Input.GetAxis("Horizontal");
        var axisY = Input.GetAxis("Vertical");
        var accelX = Input.acceleration.x;
        var accelY = Input.acceleration.y;

        var horizontal = axisX != 0 ? axisX : accelX * accelMult;
        var vertical = axisY != 0 ? axisY : accelY * accelMult;

        if (horizontal != 0 || vertical != 0)
        {
            var translationX = horizontal * speedX;
            translationX *= Time.deltaTime;

            var translationY = vertical * speedY;
            translationY *= Time.deltaTime;

            transform.Translate(translationX, translationY, 0);

            bool overflow = false;
            float x = transform.position.x;
            if (speedX != 0)
            {
                if (x < minX)
                {
                    x = minX;
                    overflow = true;
                }
                else if (x > maxX)
                {
                    x = maxX;
                    overflow = true;
                }
            }

            float y = transform.position.y;
            if (speedY != 0)
            {
                if (y < minY)
                {
                    y = minY;
                    overflow = true;
                }
                else if (y > maxY)
                {
                    y = maxY;
                    overflow = true;
                }
            }

            if (overflow)
            {
                transform.position = new Vector3(x, y, transform.position.z);
            }
        }
	}
}
