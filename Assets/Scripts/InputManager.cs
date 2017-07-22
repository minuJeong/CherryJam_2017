using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Camera m_Camera;
    [SerializeField] Player m_Player;

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
            var walkable = hit.collider.GetComponent<LevelWalkable>();
            if (walkable == null)
            {
                return;
            }

            m_Player.MoveTo(hit.point);
        }
    }
}
