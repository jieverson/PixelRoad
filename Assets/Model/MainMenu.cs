using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public AudioSource selectSound, backgroundMusic;
    public Light light;
    public GUIStyle selectedButtonStyle, normalButtonStyle;

    private enum Action { None, Start, HighScore, Exit }

    private float _centerX, _centerY, _sizeX, _sizeY, _posX;
    private Action _action, _selection;
    private float keyTimer;

    void Start()
    {
        _centerX = Screen.width / 2;
        _centerY = Screen.height / 2.2f;
        _sizeX = Screen.width / 3;
        _sizeY = Screen.height / 7;
        _posX = _centerX - _sizeX / 2;
        _action = Action.None;
        _selection = Action.Start;
        Time.timeScale = 1;
    }

    void OnGUI()
    {
        if (_action == Action.None)
        {
            if (NewGameButton())
            {
                selectSound.Play();
                _action = Action.Start;
                _selection = Action.Start;
            }
            if (HighScoreButton())
            {
                selectSound.Play();
                _action = Action.HighScore;
                _selection = Action.HighScore;
            }
            else if (ExitButton())
            {
                selectSound.Play();
                _action = Action.Exit;
                _selection = Action.Exit;
            }
        }
        else
        {
            if (_action == Action.Start)
            {
                NewGameButton();
            }
            else if (_action == Action.HighScore)
            {
                HighScoreButton();
            }
            else if (_action == Action.Exit)
            {
                ExitButton();
            }
        }
    }

    void Update()
    {
        if (_action == Action.None)
        {
            if (keyTimer < 0.15)
            {
                keyTimer += Time.deltaTime;
            }
            else
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    selectSound.Play();
                    keyTimer = 0;
                    if (_selection == Action.Start)
                        _selection = Action.Exit;
                    else if (_selection == Action.HighScore)
                        _selection = Action.Start;
                    else if (_selection == Action.Exit)
                        _selection = Action.HighScore;
                }
                if (Input.GetAxis("Vertical") < 0)
                {
                    selectSound.Play();
                    keyTimer = 0;
                    if (_selection == Action.Start)
                        _selection = Action.HighScore;
                    else if (_selection == Action.HighScore)
                        _selection = Action.Exit;
                    else if (_selection == Action.Exit)
                        _selection = Action.Start;
                }
            }
            if (Input.GetKey(KeyCode.Return) || Input.GetButton("Jump"))
            {
                selectSound.Play();
                _action = _selection;
            }
            if (Input.GetButton("Back"))
            {
                selectSound.Play();
                _selection = Action.Exit;
                _action = Action.Exit;
            }
        }
        else
        {
            backgroundMusic.volume -= 1;
            light.intensity -= 0.01f;
            
            if (!selectSound.isPlaying)
            {
                if (_action == Action.Start)
                {
                    Application.LoadLevel(1);
                }
                else if (_action == Action.HighScore)
                {
                    //_action = Action.None;
                    //backgroundMusic.volume = 1;
                    //light.intensity = 0.5f;
                    Application.LoadLevel(2);
                }
                else if (_action == Action.Exit)
                {
                    Application.Quit();
                }
            }
        }
    }

    private bool NewGameButton()
    {
        return GUI.Button(new Rect(_posX, _centerY, _sizeX, _sizeY), "new game", _selection == Action.Start ? selectedButtonStyle : normalButtonStyle);
    }

    private bool HighScoreButton()
    {
        return GUI.Button(new Rect(_posX, _centerY + _sizeY, _sizeX, _sizeY), "high score", _selection == Action.HighScore ? selectedButtonStyle : normalButtonStyle);
    }

    private bool ExitButton()
    {
        return GUI.Button(new Rect(_posX, _centerY + _sizeY * 2, _sizeX, _sizeY), "exit", _selection == Action.Exit ? selectedButtonStyle : normalButtonStyle);
    }
}
