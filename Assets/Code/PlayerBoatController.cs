using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoatController : MonoBehaviour
{

    [SerializeField] private PlayerInput playerInput;
    private Rigidbody boatRB;
    private float maxForwardSpeed = 9f;

    private float iFrames = 1f;


    [SerializeField] private int health = 5;


    [SerializeField] private MenuController menuController;

    [SerializeField] private Image healthUI;

    [SerializeField] private Transform cameraTarget;

    [SerializeField] private Transform windowPosition;

    private float mouseSensitivity = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        boatRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        calculateMovement();
        calculateCameraMovement();
    }

    private void calculateMovement()
    {

        float multiplier = 0.5f;
        //Set movement
        if (playerInput.GetCharacterMovement().y < 0)
        {
            multiplier = 0.1f;
        }


        Vector3 movement = transform.forward * playerInput.GetCharacterMovement().y;
        boatRB.AddForce(movement.normalized*multiplier, ForceMode.Impulse);



        //Adjusts turn speed based on velocity - minimised turning when slow or still (more realistic)
        Vector3 moveVelocity = new Vector3(boatRB.velocity.x, 0, boatRB.velocity.z);


        float turnSpeed = remap(moveVelocity.magnitude, 0, maxForwardSpeed, 0, 1);
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


        iFrames -= Time.deltaTime;
        if(iFrames < 0)
            iFrames = 0;


        //Check if player health is zero or boat has capsized
        if (health <= 0 || transform.up.y < -0.1)
        {
            menuController.changeScene(2);
        }




    }

    private void calculateCameraMovement()
    {
        //Debug.Log(playerInput.GetMouseMovement());

        cameraTarget.RotateAround(windowPosition.position, Vector3.up, -playerInput.GetMouseMovement().x*mouseSensitivity*Time.deltaTime * -1);

    }

    public void OnShipHit(Vector3 shipPosition)
    {

        if(iFrames == 0)
        {
            iFrames = 1;
            health--;
            healthUpdate();
        }

    }

    public float remap(float aValue, float aLow, float aHigh, float bLow, float bHigh)
    {
        float normal = Mathf.InverseLerp(aLow, aHigh, aValue);
        return Mathf.Lerp(bLow, bHigh, normal);
    }
    private void healthUpdate()
    {
        healthUI.fillAmount = remap(health, 0, 5, 0, 1);

    }


}
