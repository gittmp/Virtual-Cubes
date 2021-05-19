using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class camera : MonoBehaviour
{
    [HideInInspector]
    public Material material;
    
    [HideInInspector]
    public Vector2 RedShift = new Vector2(0.005f, 0.0f);

    [HideInInspector]
    public Vector2 GreenShift = new Vector2(0.0f, 0.0f);

    [HideInInspector]
    public Vector2 BlueShift = new Vector2(-0.005f, 0.0f);
    
    [HideInInspector]
    public int ShaderType = 0;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(ShaderType == 0 || ShaderType > 2){
            material = new Material(Shader.Find("Shaders/basic"));
        } else if(ShaderType == 1){
            material = new Material(Shader.Find("Shaders/pincushion_correction"));
        } else if(ShaderType == 2){
            material = new Material(Shader.Find("Shaders/LCA_correction"));

            material.SetVector("_RedShift", RedShift);
            material.SetVector("_GreenShift", GreenShift);
            material.SetVector("_BlueShift", BlueShift);
        }

        Graphics.Blit(source, destination, material);
    }
}
