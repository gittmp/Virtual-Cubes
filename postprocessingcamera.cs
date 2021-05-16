using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class postprocessingcamera : MonoBehaviour
{
    public Material material;
    private Camera cam;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material = new Material(Shader.Find("barrel_distortion"));
        Graphics.Blit(source, destination, material);

    }

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize += 1;
    }
}
