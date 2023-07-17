using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beziers : MonoBehaviour
{


    [SerializeField] private Vector3[] points;

    private Vector3 segmentPosition;


    // Start is called before the first frame update
    void Awake()
    {
        points = new Vector3[transform.childCount];
        for(int i = 0; i< transform.childCount; i++)
        {
            points[i] = transform.GetChild(i).position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetSection(float Time)
    {
        Time = Mathf.Clamp01(Time);
        float newTime = 1 - Time;
        Vector3 data = (newTime * newTime * newTime * points[0])
            + (3 * newTime * newTime * Time * points[1])
            + (3 * newTime * Time * Time * points[2])
            + (Time * Time * Time * points[3]);

        return Vector3.zero;
    }
}
