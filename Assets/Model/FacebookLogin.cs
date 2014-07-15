using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook;

public class FacebookLogin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LoginCallback(null);
	}
	
	// Update is called once per frame
	void Update () {
        this.renderer.enabled = !FB.IsLoggedIn;
	}

    void OnMouseDown()
    {
        FB.Login("email,publish_actions", LoginCallback);
    }

    void LoginCallback(FBResult result)
    {
        if (FB.IsLoggedIn)
        {
            PostScore(PlayerPrefs.GetInt("Last Score"));
        }
    }

    private void PostScore(int score)
    {
        var data = new Dictionary<string, string>();
        //data.Add("score", score.ToString());
        data.Add("score", "10");
        FB.API(string.Format("/{0}/scores", FB.UserId), HttpMethod.POST, callback: Bleee, formData: data);
        
    }

    void Bleee(FBResult result)
    {
        //if (result.Error != null)
            
            //erro.text = result.Text + "---" + result.Error;
    }
}
