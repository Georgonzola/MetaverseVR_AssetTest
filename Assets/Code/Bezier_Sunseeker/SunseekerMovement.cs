using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunseekerMovement : MonoBehaviour
{


    [SerializeField] Transform targetLocation;
    [SerializeField] RouteFollow targetLocationScript;

    private Rigidbody sunRB;

    private float maxForwardSpeed = 3f;
    [SerializeField] private float moveSpeed = 0.01f;


    private float distMin = 10f;
    private float distMax = 30f;
    private float speedMax = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        sunRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Stores previous rotation
        Vector2 previousRotation = new Vector2(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z);
        transform.LookAt(targetLocation.position);
        //restores rotation to previous state on the x and z axis so the boat only rotattes to look at the follow object on the y
        transform.rotation = Quaternion.Euler(previousRotation.x, transform.rotation.eulerAngles.y, previousRotation.y);

        //adds a forward force to the boat
        sunRB.AddRelativeForce(Vector3.forward * moveSpeed, ForceMode.Impulse);

        //limits maximum forward speed
        if (sunRB.velocity.magnitude > maxForwardSpeed)
        {
            sunRB.velocity = sunRB.velocity.normalized * maxForwardSpeed;
        }


        //gets the distance of the follow object and modifies it speed so that the boat cannot either lag behind or get too close
        float distance = Vector3.Distance(transform.position, targetLocation.position);
        targetLocationScript.moveSpeed = remap(distance, distMin, distMax, speedMax, 0);


    }


    public float remap(float aValue, float aLow, float aHigh, float bLow, float bHigh)
    {
        float normal = Mathf.InverseLerp(aLow, aHigh, aValue);
        return Mathf.Lerp(bLow, bHigh, normal);
    }
}
