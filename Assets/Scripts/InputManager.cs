using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] public Camera m_Camera;
    [SerializeField] public Player m_Player;

    private void Start()
    {
        Debug.Assert(m_Camera != null);
        Debug.Assert(m_Player != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnMPress();
        }
    }

    void OnMPress()
    {
        var ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hit.collider.GetComponents<Interactable>()
                .ToList()
                .ForEach((interact) => interact.Interact(m_Player, hit.point));
        }
    }


}
