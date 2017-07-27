using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DieOnHitWithSpeed : MonoBehaviour
{
    [SerializeField] public float KillSpeed;

    private float Speed;
    private Vector3 PrevPos;

    private void Update()
    {
        Speed = (transform.position - PrevPos).magnitude / Time.deltaTime;
        PrevPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        var pawn = other.GetComponent<Pawn>();
        if (pawn == null)
        {
            return;
        }

        if (Speed > KillSpeed)
        {
            pawn.Die();
        }
    }
}
