using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public GameObject m_Target;
    public float m_FollowUpFactor = 0.01F;

    private Vector3 _offset;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(m_Target != null);

        _offset = m_Target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += ((m_Target.transform.position - _offset) - transform.position) * m_FollowUpFactor;
    }
}
