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

        if (Player.Instance != null)
        {
            Player.Instance.transform.position = m_StartLocation.transform.position;
            Player.Instance.MoveTo(Player.Instance.transform.position, true);
        }
        else
        {
            if (m_InitializeSystem)
            {
                SceneManager.LoadScene("System", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}
