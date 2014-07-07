using UnityEngine;
using System.Collections;

public class AnimatedUVs : MonoBehaviour
{
    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "_MainTex";

    Vector2 uvOffset = Vector2.zero;

    public static float Speed = 1;

    void LateUpdate()
    {
        Speed += Time.deltaTime * 0.01F;

        uvOffset += (uvAnimationRate * Time.deltaTime) * Speed;
        if (renderer.enabled)
        {
            renderer.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }
    }
}