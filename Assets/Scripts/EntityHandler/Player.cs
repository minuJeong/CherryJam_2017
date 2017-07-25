using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public sealed class Player : Pawn
{
    public static Player Instance;

    public float OverrideRadius;
    public override float GetRadius()
    {
        return OverrideRadius;
    }

    public override void Interact(Interactable interactee, Vector3 contact)
    {

    }

    private void Start()
    {
        Instance = this;
    }
}
