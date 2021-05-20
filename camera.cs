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
    public int DistortionType = 0;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Attach image effects to camera based on problem
        if(DistortionType == 0 || DistortionType > 2){
            // Basic camera (no effects) for problems 0 + 3
            material = new Material(Shader.Find("Shaders/basic"));
        } else if(DistortionType == 1){
            // Apply the pincushion distortion to the rendered image (problem 1)
            material = new Material(Shader.Find("Shaders/pincushion_correction"));
        } else if(DistortionType == 2){
            // Apply LCA correction to the rendered image (problem 2)
            material = new Material(Shader.Find("Shaders/LCA_correction"));

            // Set the level of aberration
            material.SetVector("_RedShift", RedShift);
            material.SetVector("_GreenShift", GreenShift);
            material.SetVector("_BlueShift", BlueShift);
        }

        Graphics.Blit(source, destination, material);
    }
}
