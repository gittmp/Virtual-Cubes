using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class camera : MonoBehaviour
{
    public int ShaderType = 1;
    public Material material;
    public Vector2 RedShift = new Vector2(0.005f, 0.0f);
    public Vector2 GreenShift = new Vector2(0.0f, 0.0f);
    public Vector2 BlueShift = new Vector2(-0.005f, 0.0f);

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(ShaderType == 0){
            material = new Material(Shader.Find("basic"));
        } else if(ShaderType == 1){
            material = new Material(Shader.Find("pincushion_correction"));
        } else if(ShaderType == 2){
            material = new Material(Shader.Find("LCA_correction"));

            material.SetVector("_RedShift", RedShift);
            material.SetVector("_GreenShift", GreenShift);
            material.SetVector("_BlueShift", BlueShift);
        }

        Graphics.Blit(source, destination, material);
    }
}
