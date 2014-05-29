using UnityEngine;
using System.Collections;

public class ScreenOrientation : MonoBehaviour
{
    public UnityEngine.ScreenOrientation orientation = UnityEngine.ScreenOrientation.Landscape;

    void Start()
    {
        Screen.orientation = orientation;
    }
}
