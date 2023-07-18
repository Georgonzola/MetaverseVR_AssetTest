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

    [SerializeField] private float waterDrag = 0.99f;
    [SerializeField] private float angularDrag = 0.5f;

    void FixedUpdate()
    {
        //get wave height at float position
        float height = waveController.getWaveHeight(transform.position);

        if(transform.position.y < height)
        {
            //add vertical force if below water level
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y/depthBeforeSubmerged) * displacementAmount;
            floatRB.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f),transform.position, ForceMode.Acceleration);

            //add additional drag underwater
            floatRB.AddForce(displacementMultiplier * -floatRB.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            floatRB.AddTorque(displacementMultiplier * -floatRB.angularVelocity * angularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }

    }


}
