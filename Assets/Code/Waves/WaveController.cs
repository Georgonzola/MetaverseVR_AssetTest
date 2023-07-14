using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{


    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float length = 2f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float offset = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset = Time.deltaTime * speed;
    }


    public float GetWaveHeight(Vector2 wavePos)
    {
        return 0f;
    }
}
