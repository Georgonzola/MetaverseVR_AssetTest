using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuStateTextChange : MonoBehaviour
{
    private TMP_Text menuText;

    private WinStateTracker winTracker;

    private bool doSetText = true;

    void Start()
    {
        menuText = GetComponent<TMP_Text>();
        winTracker = GameObject.Find("WinStateTracker").GetComponent<WinStateTracker>();
    }

    private void LateUpdate()
    {
        if (doSetText)
        {
            //modifies the text component depending on whether or not the player achieved the objective
            if (winTracker.getWinState())
            {
                menuText.text = "You Win!";
            }
            else
            {
                menuText.text = "You Lose";
            }
            doSetText = false;
        }
    }





}
