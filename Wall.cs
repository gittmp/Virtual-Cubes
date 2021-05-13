using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Vector3 AnglePerSecond = new Vector3(0.0f, 180.0f, 0.0f);
    private GameObject cube1;

    void Start()
    {
        cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube1.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        cube1.GetComponent<Renderer>().material.color = Color.white;
        cube1.name = "Cube1";
    }

    // Update is called once per frame
    void Update()
    {
        cube1.transform.Rotate(AnglePerSecond * Time.deltaTime);
    }
}
