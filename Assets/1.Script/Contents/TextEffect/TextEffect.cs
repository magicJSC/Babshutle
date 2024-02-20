using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    Mesh mesh;

    Vector3[] vertices;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;

        vertices = mesh.vertices;

        for(int i =0;i < vertices.Length;i++)
        {
            Vector3 offset = Wave(Time.time + i);

            vertices[i] += offset;
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wave(float time)
    {
        return new Vector2(Mathf.Sin(time * 10f), Mathf.Cos(time * 5f));
    }
}
