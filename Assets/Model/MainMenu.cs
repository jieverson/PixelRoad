using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public AudioSource selectSound, backgroundMusic;

    public GUIStyle buttonStyle;

    private enum Action { None, Start, HighScore, Exit }

    private float _centerX, _centerY, _sizeX, _sizeY, _posX;

    private Action _action;

    void Start()
    {
        _action = Action.None;
        _centerX = Screen.width / 2;
        _centerY = Screen.height / 2.2f;
        _sizeX = Screen.width / 3;
        _sizeY = Screen.height / 7;
        _posX = _centerX - _sizeX / 2;
    }

    void OnGUI()
    {
        if (_action == Action.None)
        {
            if (GUI.Button(new Rect(_posX, _centerY, _sizeX, _sizeY), "new game", buttonStyle))
            {
                selectSound.Play();
                _action = Action.Start;
            }
            if (GUI.Button(new Rect(_posX, _centerY + _sizeY, _sizeX, _sizeY), "high score", buttonStyle))
            {
                selectSound.Play();
                _action = Action.HighScore;
            }
            else if (GUI.Button(new Rect(_posX, _centerY + _sizeY * 2, _sizeX, _sizeY), "exit", buttonStyle))
            {
                selectSound.Play();
                _action = Action.Exit;
            }
        }
        else
        {
            if (_action == Action.Start)
            {
                GUI.Button(new Rect(_posX, _centerY, _sizeX, _sizeY), "new game", buttonStyle);
            }
            else if (_action == Action.HighScore)
            {
                GUI.Button(new Rect(_posX, _centerY + _sizeY, _sizeX, _sizeY), "high score", buttonStyle);
            }
            else if (_action == Action.Exit)
            {
                GUI.Button(new Rect(_posX, _centerY + _sizeY * 2, _sizeX, _sizeY), "exit", buttonStyle);
            }
        }
    }

    void Update()
    {
        if (_action != Action.None)
        {
            backgroundMusic.volume -= 1;

            if (!selectSound.isPlaying)
            {
                if (_action == Action.Start)
                {
                    Application.LoadLevel(1);
                }
                else if (_action == Action.HighScore)
                {
                    _action = Action.None;
                    backgroundMusic.volume = 1;
                }
                else if (_action == Action.Exit)
                {
                    Application.Quit();
                }
            }
        }
    }
}
