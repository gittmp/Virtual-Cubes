using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class form_cubes : MonoBehaviour
{
    public Vector3 AnglePerSecond = new Vector3(0.0f, 180.0f, 0.0f);

    public List<List<GameObject>> cubes = new List<List<GameObject>>();
    public GameObject cube;
    public int NoCubesX = 3;
    public int NoCubesY = 3;

    private Camera cam;
    private float m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight;
    Shader obj_shader;
    public RenderTexture render_tex;
    GameObject plane;
    public Camera plane_camera;

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
        cam.orthographicSize = (float) Math.Max(Math.Max(NoCubesX, NoCubesY), 1);
        cam.transform.position = new Vector3((float) (NoCubesX - 1), (float) (NoCubesY - 1), -2.0f);
        cam.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        cam.rect = new Rect(m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight);
        cam.gameObject.AddComponent(typeof(camera));

        for(int x=0; x<NoCubesX; x++){
            List<GameObject> cubesY = new List<GameObject>();
            for(int y=0; y<NoCubesY; y++){
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(0.0f + 2.0f * x, 0.0f + 2.0f * y, 0.0f);
                
                cube.GetComponent<Renderer>().material.color = Color.white;
                obj_shader = Shader.Find("cubes");
                cube.GetComponent<Renderer>().material.shader = obj_shader;

                cube.name = "Cube-" + x.ToString() + '-' + y.ToString();
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
        plane_camera.orthographicSize = (float) Math.Max(Math.Max(NoCubesX, NoCubesY), 1.0);
        plane_camera.transform.position = new Vector3((float) (NoCubesX - 1), (float) (NoCubesY - 1), -7.0f);
        plane_camera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        plane_camera.rect = new Rect(m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight);
        
        // Create mesh plane
        plane = mesh_object.CreatePlane(2 * NoCubesX - 1, 2 * NoCubesY - 1);

        // Apply the (camera rendered) texture to this plane
        obj_shader = Shader.Find("cubes");
        plane.GetComponent<Renderer>().material.shader = obj_shader;
        plane.GetComponent<Renderer>().material.mainTexture = render_tex;
    }

    // Update is called once per frame
    void Update(){
        // PROBLEM 0
        for(int x=0; x<NoCubesX; x++){
            for(int y=0; y<NoCubesY; y++){
                cubes[x][y].transform.Rotate(AnglePerSecond * Time.deltaTime);
            }
        }

        if(cam.GetComponent<camera>().ShaderType == 3){
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
