using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixScript : MonoBehaviour
{
    public Material pixMat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, pixMat);
    }
}
