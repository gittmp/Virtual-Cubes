using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class postprocessingcamera : MonoBehaviour
{
    public Material material;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material = new Material(Shader.Find("pincushion_distortion"));
        Graphics.Blit(source, destination, material);

    }
}
