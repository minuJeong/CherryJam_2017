using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Walkable mesh
/// </summary>
[RequireComponent(typeof(MeshCollider))]
public class LevelWalkable : Interactable
{
    public override float GetRadius()
    {
        return 0.0F;
    }

    public override void Interact(Interactable interactee, Vector3 contact)
    {
        var pawn = interactee as Pawn;
        if (pawn != null)
        {
            pawn.MoveTo(contact, true);
        }
    }
}
