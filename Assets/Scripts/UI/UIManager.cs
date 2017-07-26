using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] public GameObject m_DeadMessage;

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    public void ShowDeadMessage()
    {
        m_DeadMessage.SetActive(true);
        var anim = m_DeadMessage.GetComponent<Animation>();
        anim.Play("DeadMessage_FadeIn", PlayMode.StopAll);
    }
}
