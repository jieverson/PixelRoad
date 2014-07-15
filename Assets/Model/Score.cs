using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{

    public float timeToScore;

    public static int points;

    private float _timer;

    void Start()
    {
        points = 0;
    }

    void Update()
    {
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
