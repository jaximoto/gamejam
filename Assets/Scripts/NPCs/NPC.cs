using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;
    public virtual void Move()
    {

    }

    public virtual void setupFov()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[3];
        uv = new Vector2[3];
        triangles = new int[3];

        vertices[0] = Vector3.zero;
        vertices[1] = new Vector3(50, 0);
        vertices[2] = new Vector3(0, -50);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public virtual void updateFov()
    {

    }

}
