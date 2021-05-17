using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class mesh_object
{
    public static GameObject CreatePlane(float width, float height)
    {
        GameObject plane = new GameObject("plane");
        MeshFilter filter = plane.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        Mesh mesh = new Mesh();
        
        mesh.vertices = new Vector3[]
        {
            new Vector3(-1.0f, -1.0f, -5.0f),
            new Vector3(width, -1.0f, -5.0f),
            new Vector3(-1.0f, height, -5.0f),
            new Vector3(width, height, -5.0f)
        };

        mesh.uv = new Vector2[]
        {
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(1.0f, 1.0f)
        };

        mesh.triangles = new int[]
        {
            2,1,0,
            1,2,3
        };

        filter.mesh = mesh;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return plane;
    }
}
