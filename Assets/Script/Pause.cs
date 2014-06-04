using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public AudioSource sound, backgroundMusic;
    public Light light;

    public static bool IsPaused;
    
    private static bool _isResuming;

	void Start () {
	    
	}
	
	void Update () {
        if ((Input.GetKeyDown(KeyCode.Escape)) || _isResuming)
        {
            _isResuming = false;

            IsPaused = !IsPaused;
            if (IsPaused)
            {
                Time.timeScale = 0;
                sound.Play();
                backgroundMusic.Pause();
                light.intensity = 0.3f;
            }
            else
            {
                Time.timeScale = 1;
                backgroundMusic.Play();
                light.intensity = 0.5f;
            }
            //Object[] objects = FindObjectsOfType(typeof(GameObject));
            //foreach (GameObject go in objects)
            //{
            //    go.SendMessage(IsPaused ? "OnPauseGame" : "OnResumeGame", SendMessageOptions.DontRequireReceiver);
            //}
        }
	}

    public static void Resume()
    {
        _isResuming = true;
    }
}
