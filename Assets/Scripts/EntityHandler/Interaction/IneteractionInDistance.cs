using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IneteractionInDistance : Interactable
{
    Coroutine _pollingPawnGetClose = null;

    public override float GetRadius()
    {
        throw new NotImplementedException();
    }

    public override void Interact(Interactable interactee, Vector3 contact)
    {
        var pawn = interactee as Pawn;
        if (pawn == null)
        {
            return;
        }

        pawn.OnWaypointReset += Pawn_OnWaypointReset;
        _pollingPawnGetClose = StartCoroutine(PollingGetClose(pawn, contact));
        pawn.MoveTo(transform.position);
    }

    private void Pawn_OnWaypointReset()
    {
        if (_pollingPawnGetClose != null)
        {
            StopCoroutine(_pollingPawnGetClose);
        }
    }

    protected virtual void OnGetClose(Interactable interactee, Vector3 contact)
    {
    }

    protected IEnumerator PollingGetClose(Interactable interactee, Vector3 contact)
    {
        while (!IsClose(interactee))
        {
            yield return null;
        }
        
        OnGetClose(interactee, contact);

        if (_pollingPawnGetClose != null)
        {
            StopCoroutine(_pollingPawnGetClose);
        }
        _pollingPawnGetClose = null;
    }
}