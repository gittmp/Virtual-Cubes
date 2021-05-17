using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class postprocessingcamera : MonoBehaviour
{
    public int ShaderType = 1;
    public Material material;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(ShaderType == 0){
            material = new Material(Shader.Find("postprocessing"));
            Graphics.Blit(source, destination, material);
        } else if(ShaderType == 1){
            material = new Material(Shader.Find("pincushion_correction"));
            Graphics.Blit(source, destination, material);
        }
    }
}
