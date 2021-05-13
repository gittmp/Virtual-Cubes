using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Vector3 AnglePerSecond = new Vector3(0.0f, 180.0f, 0.0f);

    public List<List<GameObject>> cubes = new List<List<GameObject>>();
    public GameObject cube;
    public int NoCubesX = 3;
    public int NoCubesY = 3;

    void Start(){
        for(int x=0; x<NoCubesX; x++){
            List<GameObject> cubesY = new List<GameObject>();
            for(int y=0; y<NoCubesY; y++){
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(0.0f + 2.0f * x, 0.0f + 2.0f * y, 0.0f);
                cube.GetComponent<Renderer>().material.color = Color.white;
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
