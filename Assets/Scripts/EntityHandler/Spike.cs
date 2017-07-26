using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var pawn = other.GetComponent<Pawn>();
        if (pawn == null)
        {
            return;
        }
        
        pawn.Die();
    }
}
