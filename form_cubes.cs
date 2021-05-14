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
    Shader cube_shader;
    Shader cam_shader;

    void Start(){
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
        cam.gameObject.AddComponent(typeof(postprocessingcamera));

        for(int x=0; x<NoCubesX; x++){
            List<GameObject> cubesY = new List<GameObject>();
            for(int y=0; y<NoCubesY; y++){
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(0.0f + 2.0f * x, 0.0f + 2.0f * y, 0.0f);
                
                cube.GetComponent<Renderer>().material.color = Color.white;
                cube_shader = Shader.Find("problem1a");
                cube.GetComponent<Renderer>().material.shader = cube_shader;

                cube.name = "Cube-" + x.ToString() + '-' + y.ToString();
                cubesY.Add(cube);
            }
            cubes.Add(cubesY);
        }
    }

    // Update is called once per frame
    void Update(){
        for(int x=0; x<NoCubesX; x++){
            for(int y=0; y<NoCubesY; y++){
                cubes[x][y].transform.Rotate(AnglePerSecond * Time.deltaTime);
            }
        }
    }
}
