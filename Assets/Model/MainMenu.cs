using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GUIStyle buttonStyle;

    //public GUISkin skin;

    //public GUIContent newGame;

    //public GUIContent exit;

    void OnGUI()
    {

        //GUI.Window(555, new Rect((Screen.width / 2) - 100f, (Screen.height / 2) - 60f, 200f, 120f), null, "something", GUIStyle.none);

        //var box = GUI.Box(new Rect(Screen.width / 2f, Screen.height / 3, Screen.width / 3, Screen.height / 10), "New Game");

        var centerX = Screen.width / 2;
        var centerY = Screen.height / 2.2f;
        var sizeX = Screen.width / 3;
        var sizeY = Screen.height / 7;
        var posX = centerX - sizeX / 2;

        if (GUI.Button(new Rect(posX, centerY, sizeX, sizeY), "new game", buttonStyle))
        {
            Application.LoadLevel(1);
        }
        if (GUI.Button(new Rect(posX, centerY + sizeY, sizeX, sizeY), "high score", buttonStyle))
        {

        }
        else if (GUI.Button(new Rect(posX, centerY + sizeY * 2, sizeX, sizeY), "exit", buttonStyle))
        {
            Application.Quit();
        }
    }
}
