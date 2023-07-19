using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoatController : MonoBehaviour
{
    private Rigidbody boatRB;


     private PlayerInput playerInput;
    [SerializeField] private MenuController menuController;
    [SerializeField] private Image healthUI;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform windowPosition;

    private float maxForwardSpeed = 9f;
    private float iFrames = 1f;
    private float mouseSensitivity = 2f;
    private int health = 5;


    // Start is called before the first frame update
    void Start()
    {
        boatRB = GetComponent<Rigidbody>();
        playerInput = GameObject.Find("/PlayerInputObject").GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        calculateMovement();
        calculateCameraMovement();
    }

    private void calculateMovement()
    {

        //Changes the movement speed depending on whether or not the player is moving backwards
        float multiplier = 0.5f;
        if (playerInput.GetCharacterMovement().y < 0)
        {
            multiplier = 0.15f;
        }

        //adds movement force to the boat relative to the forward vector
        Vector3 movement = transform.forward * playerInput.GetCharacterMovement().y;
        boatRB.AddForce(movement.normalized*multiplier, ForceMode.Impulse);



        //Adjusts turn speed based on velocity - minimised turning when slow or still (more realistic) also does not take vertical movement from gravity into account
        Vector2 moveVelocity = new Vector2(boatRB.velocity.x, boatRB.velocity.z);
        float turnSpeed = remap(moveVelocity.magnitude, 0, maxForwardSpeed, 0, 1);



        //Set rotation
        Vector3 rotation = transform.rotation.eulerAngles * playerInput.GetCharacterMovement().x;
        boatRB.AddRelativeTorque(rotation.normalized*2f*turnSpeed, ForceMode.Acceleration);


        //Set maximum speed
        if (boatRB.velocity.magnitude > maxForwardSpeed)
        {
            boatRB.velocity = boatRB.velocity.normalized*maxForwardSpeed;
        }

        //updates invincibility frames
        iFrames -= Time.deltaTime;
        if(iFrames < 0)
            iFrames = 0;


        //Check if player health is zero or boat has capsized, if so set the scene to the menu
        if (health <= 0 || transform.up.y < -0.1)
        {
            menuController.changeScene(2);
        }

    }

    private void calculateCameraMovement()
    {
        //moves the camera target around the window of the boat based on mouse movement
        cameraTarget.RotateAround(windowPosition.position, Vector3.up, playerInput.GetMouseMovement().x*mouseSensitivity*Time.deltaTime);

    }

    public void OnShipHit(Vector3 shipPosition)
    {
        //checks if the player still has invincibility frames to prevent too much health loss at once
        if(iFrames == 0)
        {
            iFrames = 1;
            health--;
            //updates the health bar ui
            healthUpdate();
        }

    }
    private void healthUpdate()
    {
        //changes the health bar size based on remaining health
        healthUI.fillAmount = remap(health, 0, 5, 0, 1);

    }

    private float remap(float aValue, float aLow, float aHigh, float bLow, float bHigh)
    {
        float normal = Mathf.InverseLerp(aLow, aHigh, aValue);
        return Mathf.Lerp(bLow, bHigh, normal);
    }


}
