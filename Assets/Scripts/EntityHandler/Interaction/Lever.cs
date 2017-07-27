using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Lever : IneteractionInDistance
{
    [SerializeField] public Animation m_Animation;
    [SerializeField] public InteractionTerminal m_InteractionTerminal;

    public override float GetRadius()
    {
        return GetComponent<CapsuleCollider>().radius;
    }

    protected override void OnGetClose(Interactable interactee, Vector3 contact)
    {
        if (m_InteractionTerminal != null)
        {
            m_InteractionTerminal.Interact(interactee, contact);
        }

        if (m_Animation != null)
        {
            m_Animation.Play();
        }
    }
}
