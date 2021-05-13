using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class postprocessingcamera : MonoBehaviour
{
    public Material material;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material = Resources.Load("ppmat", typeof(Material)) as Material;
        Graphics.Blit(source, destination, material);
    }
}
