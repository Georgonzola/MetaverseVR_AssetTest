using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinStateTracker : MonoBehaviour
{
    [SerializeField]
    private bool winState = false;

    void Awake()
    {
        //allows the win state to be stored across scenes which is then used to set the ui text
        DontDestroyOnLoad(this.gameObject);
    }


    public void setWinState(bool state)
    {
        winState = state;

    }

    public bool getWinState()
    {
       return winState;

    }
}
