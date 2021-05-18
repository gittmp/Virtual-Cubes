using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inverse_camera : MonoBehaviour
{
    private Camera inv_camera;
    private Camera cam;
    private int stype;

    // Start is called before the first frame update
    void Start()
    {
        inv_camera = gameObject.AddComponent(typeof(Camera)) as Camera;
        inv_camera.orthographic = true;
        inv_camera.aspect = 1.0f;
        inv_camera.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        inv_camera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        inv_camera.orthographicSize = 3.0f;
        inv_camera.transform.position = new Vector3(2.0f, 2.0f, -18.0f);
        inv_camera.targetDisplay = 2;

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        stype = cam.GetComponent<camera>().ShaderType;
        if(stype == 4){
            inv_camera.targetDisplay = 0;
        } else {
            inv_camera.targetDisplay = 2;
        }
    }
}
