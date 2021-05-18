using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class form_cubes : MonoBehaviour
{
    // SLIDER FOR SELECTING SHADER (corresponding to a different problem aim)
    public int ShaderType = 0;

    // Problem 0 parameterss
    public Vector2 NoCubes = new Vector2(3, 3);
    public Vector3 AnglePerSecond = new Vector3(0.0f, 180.0f, 0.0f);
    private List<List<GameObject>> cubes = new List<List<GameObject>>();
    private GameObject cube;
    private Camera cam;
    private Shader obj_shader;

    // Fields for camera parameters (problem 2)
    public Vector2 RedShift = new Vector2(0.005f, 0.0f);
    public Vector2 GreenShift = new Vector2(0.0f, 0.0f);
    public Vector2 BlueShift = new Vector2(-0.005f, 0.0f);

    // Problem 3 parameters
    private RenderTexture render_tex;
    private Camera plane_camera;
    private GameObject BlenderPlane;

    // Selecting plane to render to (problem 3)
    public int MeshComplexity = 0;

    // Problem 3c inverses
    private RenderTexture inv_render;
    private GameObject InvPlane;
    public Vector3 ShaderParams = new Vector3(1.0f, -1.6f, 2.5f);

    // Start method
    void Start(){
        // PROBLEM 0
        // Set main camera attributes (problem 0)
        cam = Camera.main;
        cam.orthographic = true;
        cam.aspect = 1.0f;
        cam.orthographicSize = (float) Math.Max(Math.Max(NoCubes[0], NoCubes[1]), 1);
        cam.transform.position = new Vector3((float) (NoCubes[0] - 1), (float) (NoCubes[1] - 1), -2.0f);
        cam.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

        // Add image effects to camera (problems 1 + 2)
        cam.gameObject.AddComponent(typeof(camera));

        // Build cubes (problem 0)
        for(int x=0; x<NoCubes[0]; x++){
            List<GameObject> cubesY = new List<GameObject>();
            for(int y=0; y<NoCubes[1]; y++){
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(0.0f + 2.0f * x, 0.0f + 2.0f * y, 0.0f);
                
                cube.GetComponent<Renderer>().material.color = Color.white;
                obj_shader = Shader.Find("Shaders/cubes");
                cube.GetComponent<Renderer>().material.shader = obj_shader;

                cube.name = "Cube-" + x.ToString() + '-' + y.ToString();
                cube.hideFlags = HideFlags.HideInHierarchy;
                cubesY.Add(cube);
            }
            cubes.Add(cubesY);
        }

        // PROBLEM 3
        // Create a render texture to project the camera onto the mesh
        render_tex = new RenderTexture(512, 512, 16, RenderTextureFormat.ARGB32);
        render_tex.Create();

        // Create a new camera to render the mesh to the screen
        plane_camera = gameObject.AddComponent(typeof(Camera)) as Camera;
        plane_camera.orthographic = true;
        plane_camera.aspect = 1.0f;
        plane_camera.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        plane_camera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        plane_camera.orthographicSize = 3.0f;
        plane_camera.transform.position = new Vector3(2.0f, 2.0f, -10.0f);

        // Generate plane from Blender
        BlenderPlane = createPlane(BlenderPlane);

        // PROBLEM 3c
        inv_render = new RenderTexture(512, 512, 16, RenderTextureFormat.ARGB32);
        inv_render.Create();
        InvPlane = createPlane(InvPlane, "inverse");
        InvPlane.GetComponent<Renderer>().material.SetVector("_Params", ShaderParams);

    }

    // Update method
    void Update(){
        // Rotate cubes (problem 0)
        for(int x=0; x<NoCubes[0]; x++){
            for(int y=0; y<NoCubes[1]; y++){
                cubes[x][y].transform.Rotate(AnglePerSecond * Time.deltaTime);
            }
        }
        
        // Update camera fields given LCA colour shift parameters (problem 2)
        cam.GetComponent<camera>().ShaderType = ShaderType;
        cam.GetComponent<camera>().RedShift = RedShift;
        cam.GetComponent<camera>().GreenShift = GreenShift;
        cam.GetComponent<camera>().BlueShift = BlueShift;

        // PROBLEM 3
        // Update geometry of plane (plane 1, 2, or 3) given users parameters
        BlenderPlane = createPlane(BlenderPlane);
        InvPlane = createPlane(InvPlane, "inverse");

        // for plane1 (-0.13, -1.6, 2.5)
        // for plane1 (-0.74, 0.19, 2.5)
        // for plane2 (-0.87, 0.28, 2.8)
        // for plane3 (-1.5, 0.8, 5.0)
        InvPlane.GetComponent<Renderer>().material.SetVector("_Params", ShaderParams);

        // If problem 3, set screen to display output of second camera
        if(ShaderType == 3){
            // Set the target texture of the main camera to the render texture
            cam.targetTexture = render_tex;
            plane_camera.targetDisplay = 0;
        } else if(ShaderType == 4){
            cam.targetTexture = render_tex;
            plane_camera.targetTexture = inv_render;
        } else {
            plane_camera.targetTexture = null;
            plane_camera.targetDisplay = 1;
            cam.targetTexture = null;
            cam.targetDisplay = 0;
        }
    }

    GameObject createPlane(GameObject plane, String version = "")
    {
        Destroy(plane);

        if (MeshComplexity == 0 || MeshComplexity > 2){
            plane = Instantiate(Resources.Load("Planes/plane1")) as GameObject;
            plane.transform.localScale = new Vector3(1.5f, 1.5f, 0.0f);
        } else if (MeshComplexity == 1){
            plane = Instantiate(Resources.Load("Planes/plane2")) as GameObject;
            plane.transform.localScale = new Vector3(3f, 3f, 0.0f);
        } else if (MeshComplexity == 2){
            plane = Instantiate(Resources.Load("Planes/plane3")) as GameObject;
            plane.transform.localScale = new Vector3(3f, 3f, 0.0f);
        }
        
        if(version == "inverse"){
            plane.transform.position = new Vector3(2.0f, 2.0f, -22.0f);
        } else {
            plane.transform.position = new Vector3(2.0f, 2.0f, -14.0f);
        }

        plane.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        plane.hideFlags = HideFlags.HideInHierarchy;

        // Apply the (camera rendered) texture to this plane

        if(version == "inverse"){
            obj_shader = Shader.Find("Shaders/inverse_mesh");
            plane.GetComponent<Renderer>().material.mainTexture = inv_render;
        } else {
            obj_shader = Shader.Find("Shaders/mesh_distortion");
            plane.GetComponent<Renderer>().material.mainTexture = render_tex;
        }

        plane.GetComponent<Renderer>().material.shader = obj_shader;

        return plane;
    }
}
