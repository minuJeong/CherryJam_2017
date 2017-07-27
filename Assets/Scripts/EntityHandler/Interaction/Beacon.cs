using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Beacon : IneteractionInDistance
{
    [SerializeField] public BeaconInteraction m_Interaction;
    [SerializeField] public string m_Parameter;
    
    public override float GetRadius()
    {
        return 0.5F;
    }

    protected override void OnGetClose(Interactable interactee, Vector3 contact)
    {
        switch (m_Interaction)
        {
            case BeaconInteraction.TO_LEVEL:
                SceneManager.LoadScene(m_Parameter);
                break;
        }
    }
}

public enum BeaconInteraction
{
    TO_LEVEL,
}
