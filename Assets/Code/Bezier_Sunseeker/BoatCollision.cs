using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.parent.TryGetComponent<PlayerBoatController>(out PlayerBoatController playerBoatScript))
        {
            playerBoatScript.OnShipHit(transform.position);
        }
    }
}
