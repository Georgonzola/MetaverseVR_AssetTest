using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class WaveController : MonoBehaviour
{


    private int planeDetail = 250;

    private MeshFilter meshFilter;

    private Mesh mesh;

    public WaveData[] Waves;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        mesh.name = gameObject.name;

        GenerateGeometry();

        meshFilter = this.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }



    // Update is called once per frame
    void Update()
    {
       //setWaves(); used to displace the vextices of the water mesh plane, this is now done in the shader graph to improve framerate
    }
    private void GenerateGeometry()
    {

        Vector3[] vertices = new Vector3[(planeDetail + 1) * (planeDetail + 1)];

        //spaces the vertices
        for (int x = 0; x <= planeDetail; x++)
            for (int z = 0; z <= planeDetail; z++)
                vertices[index(x, z)] = new Vector3(x, 0, z);
        //assigns the vertices to the mesh
        mesh.vertices = vertices;


        int[] tris = new int[mesh.vertices.Length * 6];

        //generates triangles from the vertices
        for (int x = 0; x < planeDetail; x++)
        {
            for (int z = 0; z < planeDetail; z++)
            {
                tris[index(x, z) * 6 + 0] = index(x, z);
                tris[index(x, z) * 6 + 1] = index(x + 1, z + 1);
                tris[index(x, z) * 6 + 2] = index(x + 1, z);
                tris[index(x, z) * 6 + 3] = index(x, z);
                tris[index(x, z) * 6 + 4] = index(x, z + 1);
                tris[index(x, z) * 6 + 5] = index(x + 1, z + 1);
            }
        }

        //sets the tris of the mesh
        mesh.triangles = tris;


        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

    }

    private int index(int x, int z)
    {
        return x * (planeDetail + 1) + z;
    } 

    //Previous wave generation code
    /*
    private void setWaves()
    {
        var verts = mesh.vertices;
        float xSpeed;
        float zSpeed;

        for (int x = 0; x <= planeDetail; x++)
        {
            for (int z = 0; z <= planeDetail; z++)
            {
                float y = 0;
                for (int w = 0; w < Waves.Length; w++)
                {
                    xSpeed = Mathf.Sin(Waves[w].waveDataDirection * Mathf.Deg2Rad);
                    zSpeed = Mathf.Cos(Waves[w].waveDataDirection * Mathf.Deg2Rad);
                    float xOffset = verts[index(x, z)].x * xSpeed;
                    float zOffset = verts[index(x, z)].z * zSpeed;
                    y += Mathf.Sin(xOffset + zOffset + Time.time * Waves[w].waveDataSpeed) * Waves[w].waveDataSize;


                }
                verts[index(x, z)].y = y;
            }
        }
        mesh.vertices = verts;
        mesh.RecalculateNormals();
    }
    */

    public float getWaveHeight(Vector3 pos)
    {
        //Gets wave height for floater position using the same sine wave math as the shader graph
        float y = 0;

        for (int w = 0; w < Waves.Length; w++)
        {
            float xSpeed = Mathf.Sin(Waves[w].waveDataDirection * Mathf.Deg2Rad);
            float zSpeed = Mathf.Cos(Waves[w].waveDataDirection * Mathf.Deg2Rad);
            float xOffset = pos.x * xSpeed;
            float zOffset = pos.z * zSpeed;
            y += Mathf.Sin(((xOffset + zOffset + Time.time * Waves[w].waveDataSpeed))) * Waves[w].waveDataSize;

        }
        return y;

    }

    //structure containing all of the data needed for the wave, this was previously used to adjust the mesh visually on runtime when it used setWaves()
    [Serializable]
    public struct WaveData
    {
        public float waveDataSpeed;
        public float waveDataDirection;
        public float waveDataSize;

    }

}
