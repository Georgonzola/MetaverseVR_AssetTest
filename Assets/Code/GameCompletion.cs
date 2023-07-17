using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompletion : MonoBehaviour
{


    private float countdownTotal = 4f;
    private float countdown = 4f;
    // Start is called before the first frame update

    private BoxCollider completionBox;

    [SerializeField] private Transform[] floaterPoints;

    [SerializeField] private MenuController menuController;

    private bool allIn = false;
    void Start()
    {
        completionBox = GetComponent<BoxCollider>();
        //floaterPoints = new Transform[4];
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

        if (allIn)
        {
            //Debug.Log(countdown);
            countdown -= Time.deltaTime;
        }
        else
        {
            countdown = countdownTotal;
        }

        if(countdown < 0)
        {
            menuController.changeScene(1);
            Debug.Log("Winner");
        }
    }
}
