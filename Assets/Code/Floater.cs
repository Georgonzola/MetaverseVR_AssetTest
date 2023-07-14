using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{


    [SerializeField] private Rigidbody floatRB;
    [SerializeField] private float depthBeforeSubmerged = 2f;
    [SerializeField] protected float displacementAmount = 3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < 0)
        {
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y/depthBeforeSubmerged)*displacementAmount;
            floatRB.AddForce(new Vector3(0f,Mathf.Abs(Physics.gravity.y) *displacementMultiplier, 0f), ForceMode.Acceleration);

        }
    }
}
