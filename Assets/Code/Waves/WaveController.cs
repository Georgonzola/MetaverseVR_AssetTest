using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class WaveController : MonoBehaviour
{


    [SerializeField] private float waveSpeed = 0.03f;
    [SerializeField] private float waveDirection = 45f;
    [SerializeField] private float waveSize = 20f;

    private int planeDetail = 250;
    private int planeSize = 1;

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

        //perlinPoints = new float[detail + 1, detail + 1];
        //for(int x = 0; x<=detail; x++)
        //    for (int y = 0; y <= detail; y++)
        //        perlinPoints[x, y] = Mathf.PerlinNoise((float)x + 0.1f, (float)y + 0.1f);


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


        var verts = new Vector3[(planeDetail + 1) * (planeDetail + 1)];

        //equaly distributed verts
        for (int x = 0; x <= planeDetail; x++)
            for (int z = 0; z <= planeDetail; z++)
                verts[index(x, z)] = new Vector3(x * planeSize, 0, z * planeSize);

        mesh.vertices = verts;


        var tries = new int[mesh.vertices.Length * 6];

        //two triangles are one tile
        for (int x = 0; x < planeDetail; x++)
        {
            for (int z = 0; z < planeDetail; z++)
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
        return x * (planeDetail + 1) + z;
    }

    private void setWaves()
    {

        var verts = mesh.vertices;
        //waveDirection = waveDirection.normalized;
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
                    //if(x == 22 && z == 0)
                    //{
                    //Debug.Log("Wave");
                    //Debug.Log(y);
                    //}
                verts[index(x, z)].y = y;

                


            }
        }

        mesh.vertices = verts;



        mesh.RecalculateNormals();
    }

    public float getWaveHeight(Vector3 pos)
    {
        float y = 0;
        //Vector3 newPos = new Vector3(Mathf.Floor(pos.x), 0, Mathf.Floor(pos.z));



        for (int w = 0; w < Waves.Length; w++)
        {
            float xSpeed = Mathf.Sin(Waves[w].waveDataDirection * Mathf.Deg2Rad);
            float zSpeed = Mathf.Cos(Waves[w].waveDataDirection * Mathf.Deg2Rad);
            float xOffset = pos.x * xSpeed;
            float zOffset = pos.z * zSpeed;
            y += Mathf.Sin((xOffset + zOffset + Time.time * Waves[w].waveDataSpeed)) * Waves[w].waveDataSize;

        }
        return y;

    }



    [Serializable]
    public struct WaveData
    {
        public float waveDataSpeed;
        public float waveDataDirection;
        public float waveDataSize;
    }

}
