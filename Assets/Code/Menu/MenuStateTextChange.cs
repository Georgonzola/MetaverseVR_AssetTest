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
            
            if(winTracker == null) {
                winTracker = GameObject.Find("WinStateTracker").GetComponent<WinStateTracker>();
                Debug.Log("resetReference");
            }

            if (winTracker.getWinState())
            {
                menuText.text = "You Win!";
            }
            else
            {
                menuText.text = "You Died";
            }
            doSetText = false;
        }
    }





}
