using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{


    [SerializeField] private Rigidbody floatRB;
    [SerializeField] private float depthBeforeSubmerged = 2f;
    [SerializeField] protected float displacementAmount = 3f;

    [SerializeField] private WaveController waveController;

    // Start is called before the first frame update
    void Start()
    {
        floatRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //float height = waveController.getWaveHeight(transform.position);

        //transform.position = new Vector3(transform.position.x, height, transform.position.z);


        //if(transform.position.y < height)
        //{
        //    float displacementMultiplier = Mathf.Clamp01(-transform.position.y/depthBeforeSubmerged)*displacementAmount;
        //    floatRB.AddForce(new Vector3(0f,Mathf.Abs(Physics.gravity.y) *displacementMultiplier, 0f), ForceMode.Acceleration);

        //}

    }
}
