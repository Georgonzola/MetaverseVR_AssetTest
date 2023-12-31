using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform targetLook;
    [SerializeField] private Transform targetPosition;

    [SerializeField] private float smoothTime;

    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Awake()
    {
        transform.position = targetPosition.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //makes the camera look at the window of the boat - ideal focus point
        transform.LookAt(targetLook);
        //smoothly moves the camera to the camera target child of the boat
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition.position, ref velocity, smoothTime);
    }
}
