using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Beacon : Interactable
{
    [SerializeField] public BeaconInteraction m_Interaction;
    [SerializeField] public string m_Parameter;

    Coroutine _pollingPawnGetClose = null;

    public override float GetRadius()
    {
        return 0.5F;
    }

    public override void Interact(Interactable interactee, Vector3 contact)
    {
        var pawn = interactee as Pawn;
        if (pawn == null)
        {
            return;
        }

        pawn.MoveTo(transform.position);
        pawn.OnWaypointReset += Pawn_OnWaypointReset;
        _pollingPawnGetClose = StartCoroutine(PollingGetClose(pawn.gameObject));
    }

    private void Pawn_OnWaypointReset()
    {
        if (_pollingPawnGetClose != null)
        {
            StopCoroutine(_pollingPawnGetClose);
        }
    }

    protected void OnGetClose()
    {
        switch (m_Interaction)
        {
            case BeaconInteraction.TO_LEVEL:
                if (_pollingPawnGetClose != null)
                {
                    StopCoroutine(_pollingPawnGetClose);
                }
                _pollingPawnGetClose = null;

                SceneManager.LoadScene(m_Parameter);
                break;
        }
    }

    protected IEnumerator PollingGetClose(GameObject target)
    {
        while (true)
        {
            yield return null;

            if ((transform.position - target.transform.position).magnitude < 0.5F)
            {
                OnGetClose();
                break;
            }
        }
    }
}

public enum BeaconInteraction
{
    TO_LEVEL,
}
