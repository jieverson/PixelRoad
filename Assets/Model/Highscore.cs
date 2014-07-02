using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Highscore : MonoBehaviour
{

    public GUIText current, first, second, third;

    public float flashSpeed = 0.1F;

    private bool flash = true;

    private float _flashTimer;

    void Start()
    {
        CheckDummyScore();

        current.text = PlayerPrefs.GetInt("Last Score").ToString();
        first.text = PlayerPrefs.GetInt("First Score").ToString();
        second.text = PlayerPrefs.GetInt("Second Score").ToString();
        third.text = PlayerPrefs.GetInt("Third Score").ToString();
    }

    void Update()
    {
        if (Input.anyKey || Input.touchCount > 0)
        {
            Application.LoadLevel(0);
        }

        var scores = new List<GUIText> { first, second, third };
        var flashing = scores.LastOrDefault(x => x.text == current.text);

        if (flashing != null)
        {

            _flashTimer += Time.deltaTime;
            if (_flashTimer > flashSpeed)
            {
                _flashTimer -= flashSpeed;
                flash = !flash;
            }

            if (flash)
            {
                flashing.color = Color.white;
            }
            else
            {
                flashing.color = Color.gray;
            }
        }
    }

    public static void UpdateScore(int score)
    {
        PlayerPrefs.SetInt("Last Score", score);

        CheckDummyScore();

        var scores = new List<int> { 
            PlayerPrefs.GetInt("First Score"),
            PlayerPrefs.GetInt("Second Score"),
            PlayerPrefs.GetInt("Third Score") 
        };

        var wins = scores.Count(x => score > x);
        scores.Insert(3 - wins, score);

        PlayerPrefs.SetInt("First Score", scores[0]);
        PlayerPrefs.SetInt("Second Score", scores[1]);
        PlayerPrefs.SetInt("Third Score", scores[2]);
    }

    //se ainda n tem scores gravados, cria dummy scores
    private static void CheckDummyScore()
    {
        if (!PlayerPrefs.HasKey("Last Score"))
            PlayerPrefs.SetInt("Last Score", 0);

        if (!PlayerPrefs.HasKey("First Score"))
            PlayerPrefs.SetInt("First Score", 800);

        if (!PlayerPrefs.HasKey("Second Score"))
            PlayerPrefs.SetInt("Second Score", 500);

        if (!PlayerPrefs.HasKey("Third Score"))
            PlayerPrefs.SetInt("Third Score", 100);
    }
}
