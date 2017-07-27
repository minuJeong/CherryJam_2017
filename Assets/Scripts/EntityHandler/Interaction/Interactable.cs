using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Interactable with click/drag
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    public abstract float GetRadius();
    public abstract void Interact(Interactable interactee, Vector3 contact);

    protected bool IsClose(Interactable target)
    {
        var targetDistance = target.GetRadius() + GetRadius();
        return (transform.position - target.transform.position).magnitude < targetDistance;
    }
}
