using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void changeScene(int sceneIndex)
    {


        SceneManager.LoadScene(sceneIndex);
        
        if (sceneIndex == 1)
        {
            //resets the win state when returning back to the game scene
            WinStateTracker winTracker = GameObject.Find("/WinStateTracker").GetComponent<WinStateTracker>();
            winTracker.setWinState(false);
        }

    }
}
