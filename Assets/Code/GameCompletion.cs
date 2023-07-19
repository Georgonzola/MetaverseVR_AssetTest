using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompletion : MonoBehaviour
{


    private float countdownTotal = 3f;
    private float countdown = 3f;
    // Start is called before the first frame update

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
        completionBox = GetComponent<BoxCollider>();
        yellow = new Color32(244, 255, 92, 1);
        currentColour = yellow;
        compMat.SetColor("_Colour", yellow);
    }

    // Update is called once per frame
    void Update()
    {
        allIn = true;
        for(int i = 0; i < floaterPoints.Length; i++)
        {
            if (!completionBox.bounds.Contains(floaterPoints[i].position))
            {
                allIn = false;
            }
        }

        if (allIn && player.forward.z < 0)
        {
           // Debug.Log(countdown);
            countdown -= Time.deltaTime;
            currentColour.r = (byte)remap(countdown, countdownTotal, 0, 244, green);
            compMat.SetColor("_Colour", currentColour);
        }
        else
        {
            countdown = countdownTotal;
            compMat.SetColor("_Colour", yellow);
        }

        if(countdown < 0)
        {
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
