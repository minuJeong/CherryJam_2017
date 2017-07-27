using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPlatform : InteractionTerminal
{
    [SerializeField] public Collider m_PlatformCollider;
    [SerializeField] public Animation m_Animation;

    public override void Interact(Interactable interactee, Vector3 contact)
    {
        if (m_PlatformCollider != null)
        {
            m_PlatformCollider.enabled = false;
        }

        if (m_Animation != null)
        {
            m_Animation.Play();
        }
    }
}
