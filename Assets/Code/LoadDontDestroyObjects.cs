using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDontDestroyObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Moves straight to the game scene once the dont destroy objects are loaded
        SceneManager.LoadScene(1);
    }


}
