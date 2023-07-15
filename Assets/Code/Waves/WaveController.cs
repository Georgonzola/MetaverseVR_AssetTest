using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class WaveController : MonoBehaviour
{


    [SerializeField] private float offset = 80f;
    [SerializeField] private float intensity = 10f;
    // private MeshFilter meshFilter;
    [SerializeField] private int detail = 100;
    [SerializeField] private int size = 10;
    [SerializeField] private float waveSize = 8f;

    private MeshFilter meshFilter;

    private Mesh mesh;


    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        mesh.name = gameObject.name;

        GenerateGeometry();

        meshFilter = this.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        //transform.Translate(new Vector3(-detail/2*size, 0, -detail/2*size));
    }



    // Update is called once per frame
    void Update()
    {
        //offset += Time.deltaTime * speed;
        setWaves();

    }
    private void GenerateGeometry()
    {
        

        var verts = new Vector3[(detail+1)*(detail+1)];

        //equaly distributed verts
        for (int x = 0; x <= detail; x++)
            for (int z = 0; z <= detail; z++)
                verts[index(x, z)] = new Vector3(x*size, 0, z*size);

        mesh.vertices = verts;


        var tries = new int[mesh.vertices.Length * 6];

        //two triangles are one tile
        for (int x = 0; x < detail; x++)
        {
            for (int z = 0; z < detail; z++)
            {
                tries[index(x, z) * 6 + 0] = index(x, z);
                tries[index(x, z) * 6 + 1] = index(x + 1, z + 1);
                tries[index(x, z) * 6 + 2] = index(x + 1, z);
                tries[index(x, z) * 6 + 3] = index(x, z);
                tries[index(x, z) * 6 + 4] = index(x, z + 1);
                tries[index(x, z) * 6 + 5] = index(x + 1, z + 1);
            }
        }

        mesh.triangles = tries;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

    }

    private int index(int x, int z)
    {
        return x * (detail+1) + z;
    }

    private void setWaves()
    {
        var verts = mesh.vertices;
        for (int x = 0; x <= detail; x++)
        {
            for (int z = 0; z <= detail; z++)
            {
                var y = 0f;
                
                var perl = Mathf.PerlinNoise(((float)x+0.1f)*waveSize,((float)z+0.1f)*waveSize) * intensity;
                y += Mathf.Cos(perl + 2 * Time.time)*0.5f;
                perl = Mathf.PerlinNoise(((float)x + 0.1f + offset)*waveSize, ((float)z + 0.1f + offset)*waveSize) * intensity;
                y += Mathf.Cos(perl + Time.time + 2f)*0.7f;
                verts[index(x, z)] = new Vector3(x*size, y, z*size);
            }
        }
        mesh.vertices = verts;
        mesh.RecalculateNormals();
    }

    public float getWaveHeight(Vector3 pos) 
    {
        float closestDistance = Mathf.Infinity;
        Vector3 closestVertex = Vector3.zero;
        for(int i=0; i<mesh.vertices.Length; i++)
        {
            float dist = Vector3.Distance(pos,mesh.vertices[i]);
            if (dist <= closestDistance)
            {
                closestDistance = dist;
                closestVertex = mesh.vertices[i];
            }
        }
        return closestVertex.y;

    }


}
