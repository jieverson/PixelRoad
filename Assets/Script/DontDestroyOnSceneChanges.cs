using UnityEngine;
using System.Collections;

public class DontDestroyOnSceneChanges : MonoBehaviour
{

    public AudioClip SoundClip;
    private AudioSource SoundSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SoundSource = gameObject.GetComponent<AudioSource>();
        //SoundSource.playOnAwake = false;
        //SoundSource.rolloffMode = AudioRolloffMode.Logarithmic;
        //SoundSource.loop = true;
    }

    void Start()
    {
        //SoundSource.clip = SoundClip;
        //SoundSource.Play();
    }
}
