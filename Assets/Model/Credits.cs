using UnityEngine;
using System.Collections;
using UnityGoogleAnalytics;

public class Credits : MonoBehaviour {

    public string emailToContact, emailSubject;
    public GUIStyle buttonStyle;

    void OnGUI()
    {
        if (NewGameButton())
        {
            Track.Event("Contact", "Ok");
            Application.OpenURL("mailto:" + emailToContact + "?subject=" + emailSubject);
        }
    }

	void Update () {
        if (Input.GetButton("Back"))
        {
            Application.LoadLevel(0);
        }
	}

    private bool NewGameButton()
    {
        return GUI.Button(new Rect(0, Screen.height - 50, Screen.width, 30), "contact", buttonStyle);
    }
}
