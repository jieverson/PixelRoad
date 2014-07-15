using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityGoogleAnalytics;
using Facebook;

public class Highscore : MonoBehaviour
{

    public GUIText current, first, second, third;

    public float flashSpeed = 0.1F;

    public Transform loginButton;

    private bool flash = true;

    private float _flashTimer;

    bool login;

    void Start()
    {
        login = false;
        CheckDummyScore();

        current.text = PlayerPrefs.GetInt("Last Score").ToString();
        first.text = PlayerPrefs.GetInt("First Score").ToString();
        second.text = PlayerPrefs.GetInt("Second Score").ToString();
        third.text = PlayerPrefs.GetInt("Third Score").ToString();

        //if (FB.IsLoggedIn)
        //{
        //    PostScore(PlayerPrefs.GetInt("Last Score"));
        //}
    }

    void Update()
    {
        //CheckLoginClick();

        if (Input.anyKey || Input.touchCount > 0)
        //if (Input.GetButtonDown("Back"))
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
            PlayerPrefs.SetInt("First Score", 0);

        if (!PlayerPrefs.HasKey("Second Score"))
            PlayerPrefs.SetInt("Second Score", 0);

        if (!PlayerPrefs.HasKey("Third Score"))
            PlayerPrefs.SetInt("Third Score", 0);
    }

    // Facebook
    //private void CheckLoginClick()
    //{
    //    loginButton.renderer.enabled = !FB.IsLoggedIn;

    //    if (!FB.IsLoggedIn)
    //    {
    //        if (Input.touchCount > 0)
    //        {
    //            if (Input.GetTouch(0).phase == TouchPhase.Began)
    //            {
    //                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
    //                if (hitInfo)
    //                {
    //                    if (hitInfo.transform == loginButton)
    //                    {
    //                        Debug.Log("Touch");
    //                        login = true;
    //                        FB.Login("email,publish_actions", LoginCallback);
    //                        Track.Event("Login", "Highscore");
    //                    }
    //                }
    //            }
    //        }

    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            //Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetMouseButtonDown(0).);
    //            Vector2 touchPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //            if (collider2D == Physics2D.OverlapPoint(touchPos))
    //            {
    //                Debug.Log(Physics2D.OverlapPoint(touchPos));
    //            }

    //            RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(Input.mousePosition.x, Input.mousePosition.y), Vector2.zero);               
    //            if (hitInfo)
    //            {
    //                if (hitInfo.transform == loginButton)
    //                {
    //                    Debug.Log("Click");
    //                    login = true;
    //                    FB.Login("email,publish_actions", LoginCallback);
    //                    Track.Event("Login", "Highscore");
    //                }
    //            }
    //            else
    //            {
    //                Debug.Log("no");
    //            }
    //        }
    //    }
    //}

    //void LoginCallback(FBResult result)
    //{
    //    if (FB.IsLoggedIn)
    //    {
    //        PostScore(PlayerPrefs.GetInt("Last Score"));
    //    }
    //}

    //private void PostScore(int score)
    //{
    //    var data = new Dictionary<string, string>();
    //    data.Add("score", score.ToString());
    //    FB.API(string.Format("/{0}/scores", FB.UserId), HttpMethod.POST, formData: data);
    //    Debug.Log("Pos Score");
    //}
    
}
