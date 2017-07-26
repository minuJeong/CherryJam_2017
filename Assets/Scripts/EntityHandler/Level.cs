using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] public GameObject m_StartLocation;
    [SerializeField] public bool m_InitializeSystem = false;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(m_StartLocation != null);

        // Simple Level Replacement
        if (Player.Instance != null)
        {
            Player.Instance.m_Agent.enabled = false;
            Player.Instance.transform.position = m_StartLocation.transform.position;
            Player.Instance.m_Agent.enabled = true;
            Player.Instance.MoveTo(Player.Instance.transform.position, true);
        }

        // Load from start
        else
        {
            // On fitst level
            if (m_InitializeSystem)
            {
                SceneManager.LoadScene("System", LoadSceneMode.Additive);
            }

            // Conveniently start game from the first level
            else
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}
