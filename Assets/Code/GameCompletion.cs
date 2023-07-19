using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompletion : MonoBehaviour
{


    private float countdownTotal;
    private float countdown = 3f;

    private BoxCollider completionBox;

    [SerializeField] private Transform[] floaterPoints;
    [SerializeField] private MenuController menuController;
    [SerializeField] private Material compMat;
    [SerializeField] private Transform player;


    private Color32 yellow;
    private Color32 currentColour;


    private float green = 95f;
    private bool allIn = false;
    
    void Awake()
    {
        countdownTotal = countdown;
        completionBox = GetComponent<BoxCollider>();
        yellow = new Color32(244, 255, 92, 1);
        currentColour = yellow;
        compMat.SetColor("_Colour", yellow);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks whether the four bounding points of the boat are within the completion zone
        allIn = true;
        for(int i = 0; i < floaterPoints.Length; i++)
        {
            if (!completionBox.bounds.Contains(floaterPoints[i].position))
            {
                allIn = false;
            }
        }


        //getting the player forward position to prevent the player from triggering completion when facing the wrong way
        //ideally this should be done by comparing it to the completion box's forward vector to allow the code to be used regardless of the completion box's rotation
        if (allIn && player.forward.z < 0)
        {
            //decrease timer and scale colour to green
            countdown -= Time.deltaTime;
            currentColour.r = (byte)remap(countdown, countdownTotal, 0, 244, green);
            compMat.SetColor("_Colour", currentColour);
        }
        else
        {

            //reset timer and colour
            countdown = countdownTotal;
            compMat.SetColor("_Colour", yellow);
        }

        if(countdown < 0)
        {
            //trigger win state on the dont destroy tracker and change scenes
            WinStateTracker winTracker = GameObject.Find("WinStateTracker").GetComponent<WinStateTracker>();
            winTracker.setWinState(true);
            menuController.changeScene(2);
        }
    }

    public float remap(float aValue, float aLow, float aHigh, float bLow, float bHigh)
    {
        float normal = Mathf.InverseLerp(aLow, aHigh, aValue);
        return Mathf.Lerp(bLow, bHigh, normal);
    }
}
