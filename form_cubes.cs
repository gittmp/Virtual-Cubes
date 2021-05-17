using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class form_cubes : MonoBehaviour
{
    // SLIDER FOR SELECTING SHADER (corresponding to a different problem aim)
    public int ShaderType = 0;

    // Problem 0 parameters
    public Vector2 NoCubes = new Vector2(3, 3);
    public Vector3 AnglePerSecond = new Vector3(0.0f, 180.0f, 0.0f);
    private List<List<GameObject>> cubes = new List<List<GameObject>>();
    private GameObject cube;
    private Camera cam;
    private float m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight;
    private Shader obj_shader;

    // Problem 3 parameters
    private RenderTexture render_tex;
    private GameObject plane;
    private Camera plane_camera;

    // Fields for camera parameters (problem 2)
    public Vector2 RedShift = new Vector2(0.005f, 0.0f);
    public Vector2 GreenShift = new Vector2(0.0f, 0.0f);
    public Vector2 BlueShift = new Vector2(-0.005f, 0.0f);

    // Start method
    void Start(){
        // PROBLEM 0
        //This sets the Camera view rectangle to be in the bottom corner of the screen
        m_ViewPositionX = 0.0f;
        m_ViewPositionY = 0.0f;

        //This sets the Camera view rectangle size
        m_ViewWidth = 1.0f;
        m_ViewHeight = 1.0f;

        cam = Camera.main;
        cam.orthographic = true;
        cam.aspect = 1.0f;
        cam.orthographicSize = (float) Math.Max(Math.Max(NoCubes[0], NoCubes[1]), 1);
        cam.transform.position = new Vector3((float) (NoCubes[0] - 1), (float) (NoCubes[1] - 1), -2.0f);
        cam.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        cam.rect = new Rect(m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight);
        cam.gameObject.AddComponent(typeof(camera));

        for(int x=0; x<NoCubes[0]; x++){
            List<GameObject> cubesY = new List<GameObject>();
            for(int y=0; y<NoCubes[1]; y++){
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(0.0f + 2.0f * x, 0.0f + 2.0f * y, 0.0f);
                
                cube.GetComponent<Renderer>().material.color = Color.white;
                obj_shader = Shader.Find("cubes");
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
        plane_camera.orthographicSize = (float) Math.Max(Math.Max(NoCubes[0], NoCubes[1]), 1.0);
        plane_camera.transform.position = new Vector3((float) (NoCubes[0] - 1), (float) (NoCubes[1] - 1), -7.0f);
        plane_camera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        plane_camera.rect = new Rect(m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight);

        // Create mesh plane
        plane = mesh_object.CreatePlane(2 * NoCubes[0] - 1, 2 * NoCubes[1] - 1);
        plane.hideFlags = HideFlags.HideInHierarchy;

        // Apply the (camera rendered) texture to this plane
        obj_shader = Shader.Find("mesh_distortion");
        plane.GetComponent<Renderer>().material.shader = obj_shader;
        plane.GetComponent<Renderer>().material.mainTexture = render_tex;
    }

    // Update method
    void Update(){
        // Update camera fields
        cam.GetComponent<camera>().ShaderType = ShaderType;
        cam.GetComponent<camera>().RedShift = RedShift;
        cam.GetComponent<camera>().GreenShift = GreenShift;
        cam.GetComponent<camera>().BlueShift = BlueShift;

        // PROBLEM 0
        for(int x=0; x<NoCubes[0]; x++){
            for(int y=0; y<NoCubes[1]; y++){
                cubes[x][y].transform.Rotate(AnglePerSecond * Time.deltaTime);
            }
        }

        // PROBLEM 3
        if(ShaderType == 3){
            // Set the target texture of the main camera to the render texture
            cam.targetTexture = render_tex;
            plane_camera.targetDisplay = 0;
        } else {
            plane_camera.targetDisplay = 1;
            cam.targetTexture = null;
            cam.targetDisplay = 0;
        }
    }
}
