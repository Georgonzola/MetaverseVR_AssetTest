using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Floater : MonoBehaviour
{


    [SerializeField] private Rigidbody floatRB;
    [SerializeField] private float depthBeforeSubmerged = 1f;
    [SerializeField] protected float displacementAmount = 3f;

    [SerializeField] private WaveController waveController;

    //[SerializeField] private int numFloaters = 4;
    [SerializeField] private float waterDrag = 0.99f;
    [SerializeField] private float angularDrag = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //floatRB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //floatRB.AddForceAtPosition(new Vector3(0f,Physics.gravity.y/numFloaters,0f), transform.position, ForceMode.Acceleration);


        float height = waveController.getWaveHeight(transform.position);
       // transform.position = new Vector3(position.x, height, position.y);

        if(transform.position.y < height)
        {
            //Debug.Log("Below");
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y/depthBeforeSubmerged) * displacementAmount;
            floatRB.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f),transform.position, ForceMode.Acceleration);



            floatRB.AddForce(displacementMultiplier * -floatRB.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            floatRB.AddTorque(displacementMultiplier * -floatRB.angularVelocity * angularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }

    }


}
