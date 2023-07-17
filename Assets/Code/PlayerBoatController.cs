using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoatController : MonoBehaviour
{

    [SerializeField] private PlayerInput playerInput;
    private Rigidbody boatRB;
    private float maxForwardSpeed = 10f;
    private float maxReverseSpeed = 5f;

    private float iFrames = 1f;


    [SerializeField] private int health = 10;


    [SerializeField] private MenuController menuController;

    // Start is called before the first frame update
    void Start()
    {
        boatRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        calculateMovement();
    }

    private void calculateMovement()
    {
     
        //Set movement
        Vector3 movement = transform.forward * playerInput.GetCharacterMovement().y;
        boatRB.AddForce(movement.normalized*0.5f, ForceMode.Impulse);



        //Adjusts turn speed based on velocity - minimised turning when slow or still (more realistic)
        Vector3 moveVelocity = new Vector3(boatRB.velocity.x, 0, boatRB.velocity.z);


        float turnSpeed = remap(moveVelocity.magnitude, maxReverseSpeed*2, maxForwardSpeed, -1, 1);
        if (turnSpeed < 0) { turnSpeed *= -1; }



        //Set rotation
        Vector3 rotation = transform.rotation.eulerAngles * playerInput.GetCharacterMovement().x;
        boatRB.AddRelativeTorque(rotation.normalized*2f*turnSpeed, ForceMode.Acceleration);




        //Reorient rotation to follow forward vector (more realistic)
        //Vector3 steerVelocity = transform.forward * moveVelocity.magnitude;
        //boatRB.velocity = new Vector3 (steerVelocity.x, boatRB.velocity.y, steerVelocity.z);


        //Set maximum speed

        if (boatRB.velocity.magnitude > maxForwardSpeed)
        {
            boatRB.velocity = boatRB.velocity.normalized*maxForwardSpeed;
        }

        //if (boatRB.velocity.magnitude < maxReverseSpeed)
        //{
        //    boatRB.velocity = boatRB.velocity.normalized * maxReverseSpeed;
        //}

        iFrames -= Time.deltaTime;
        if(iFrames < 0)
            iFrames = 0;


        if (health <= 0)
        {
            menuController.changeScene(1);
        }

    }

    public void OnShipHit(Vector3 shipPosition)
    {
        //Vector3 pushDirection = shipPosition - transform.position;
        //boatRB.AddForce(-pushDirection*1000f, ForceMode.Force);
        if(iFrames == 0)
        {
            //Debug.Log("HIT");
            iFrames = 1;
            health--;
        }

    }

    public float remap(float aValue, float aLow, float aHigh, float bLow, float bHigh)
    {
        float normal = Mathf.InverseLerp(aLow, aHigh, aValue);
        return Mathf.Lerp(bLow, bHigh, normal);
    }
}
