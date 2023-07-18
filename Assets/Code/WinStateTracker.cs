using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinStateTracker : MonoBehaviour
{
    [SerializeField]
    private bool winState = false;


    //private bool setText = true;
    // Start is called before the first frame update
    void Awake()
    {
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
