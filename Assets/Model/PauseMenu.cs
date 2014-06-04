using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public AudioSource selectSound;
    public GUIStyle selectedButtonStyle, normalButtonStyle;

    private enum Action { None, Resume, Exit }

    private float _centerX, _centerY, _sizeX, _sizeY, _posX;
    private Action _action, _selection;

	void Start () {
        _centerX = Screen.width / 2;
        _centerY = Screen.height / 2.2f;
        _sizeX = Screen.width / 3;
        _sizeY = Screen.height / 7;
        _posX = _centerX - _sizeX / 2;
        _action = Action.None;
        _selection = Action.Resume;
	}

    void OnGUI()
    {
        if (Pause.IsPaused)
        {
            if (_action == Action.None)
            {
                if (ResumeButton())
                {
                    _selection = Action.Resume;
                    Pause.Resume();
                    _action = Action.None;
                    
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
                if (_action == Action.Resume)
                {
                    ResumeButton();
                }
                else if (_action == Action.Exit)
                {
                    ExitButton();
                }
            }
        }
    }
	
	void Update () {
        if (Pause.IsPaused)
        {
            if (_action == Action.None)
            {
                if(!selectSound.isPlaying){
                    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                    {
                        selectSound.Play();
                        if (_selection == Action.Resume)
                            _selection = Action.Exit;
                        else if (_selection == Action.Exit)
                            _selection = Action.Resume;
                    }
                }
                if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
                {
                    _action = _selection;
                    if (_action == Action.Resume)
                    {
                        Pause.Resume();
                        _action = Action.None;
                    }
                    else
                    {
                        selectSound.Play();
                    }
                }
            }
            else
            {
                if (!selectSound.isPlaying)
                {
                    Pause.Resume();
                    if (_action == Action.Exit)
                    {
                        Application.LoadLevel(0);
                    }
                }
            }
        }
	}

    private bool ResumeButton()
    {
        return GUI.Button(new Rect(_posX, _centerY - _sizeY, _sizeX, _sizeY), "resume", _selection == Action.Resume ? selectedButtonStyle : normalButtonStyle);
    }

    private bool ExitButton()
    {
        return GUI.Button(new Rect(_posX, _centerY, _sizeX, _sizeY), "exit", _selection == Action.Exit ? selectedButtonStyle : normalButtonStyle);
    }
}
