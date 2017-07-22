using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;
    public NavMeshAgent m_Agent
    {
        get
        {
            if (_agent == null)
            {
                _agent = GetComponent<NavMeshAgent>();
            }
            return _agent;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveTo(Vector3 pos)
    {
        m_Agent.SetDestination(pos);
    }
}
