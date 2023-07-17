using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierVisualiser : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;
    //[SerializeField] private int numSections = 20;
    //private float distanceStep;

    private Vector3 currentPosition;
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(controlPoints[0].position.x, controlPoints[0].position.y, controlPoints[0].position.z), new Vector3(controlPoints[1].position.x, controlPoints[1].position.y, controlPoints[1].position.z));
        Gizmos.DrawLine(new Vector3(controlPoints[2].position.x, controlPoints[2].position.y, controlPoints[2].position.z), new Vector3(controlPoints[3].position.x, controlPoints[3].position.y, controlPoints[3].position.z));
        Handles.DrawBezier(controlPoints[0].position, controlPoints[3].position, controlPoints[1].position, controlPoints[2].position, Color.white, null, 3f);
    }


    void Awake()
    {
    }

}
