using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoatController : MonoBehaviour
{

    [SerializeField] private PlayerInput playerInput;
    private Rigidbody boatRB;
    private float maxForwardSpeed = 25f;
    private float maxReverseSpeed = -10f;



    // Start is called before the first frame update
    void Start()
    {
        boatRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
    }

    private void calculateMovement()
    {
     
        //Set movement
        Vector3 movement = transform.forward * playerInput.GetCharacterMovement().y;
        boatRB.AddForceAtPosition(movement.normalized*0.5f, transform.position, ForceMode.Impulse);



        //Adjusts turn speed based on velocity - minimised turning when slow or still (more realistic)
        Vector3 moveVelocity = new Vector3(boatRB.velocity.x, 0, boatRB.velocity.z);
        float turnSpeed = remap(moveVelocity.magnitude, maxReverseSpeed*2, maxForwardSpeed, -1, 1);
        if (turnSpeed < 0) { turnSpeed *= -1; }

        //Set rotation
        Vector3 rotation = transform.rotation.eulerAngles * playerInput.GetCharacterMovement().x;
        boatRB.AddTorque(rotation.normalized*0.05f*turnSpeed, ForceMode.Impulse);

        //Reorient rotation to follow forward vector (more realistic)
        Vector3 steerVelocity = transform.forward * moveVelocity.magnitude;
        boatRB.velocity = new Vector3 (steerVelocity.x, boatRB.velocity.y, steerVelocity.z);


        //Set maximum speed
        if(boatRB.velocity.magnitude > maxForwardSpeed)
        {
            boatRB.velocity = boatRB.velocity.normalized*maxForwardSpeed;
        }

        if (boatRB.velocity.magnitude < maxReverseSpeed)
        {
            boatRB.velocity = boatRB.velocity.normalized * maxReverseSpeed;
        }

    }



    public float remap(float aValue, float aLow, float aHigh, float bLow, float bHigh)
    {
        float normal = Mathf.InverseLerp(aLow, aHigh, aValue);
        return Mathf.Lerp(bLow, bHigh, normal);
    }
}
